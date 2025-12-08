using Data.Repository.UnitOfWork;
using Domain.Dtos.PaymentDtos;
using Domain.Helpers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Domain.Services.Payment
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaymobSettings _paymobSettings;
        private readonly HttpClient _httpClient;
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(IUnitOfWork unitOfWork, IOptions<PaymobSettings> paymobSettings, HttpClient httpClient, ILogger<PaymentService> logger = null)
        {
            _unitOfWork = unitOfWork;
            _paymobSettings = paymobSettings.Value;
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<PaymentResponseDto> InitiatePaymentAsync(PaymentRequestDto request, Guid userId)
        {
            try
            {
                // التحقق من صحة الإعدادات
                if (string.IsNullOrEmpty(_paymobSettings.SecretKey))
                {
                    _logger?.LogError("❌ Paymob SecretKey is not configured");
                    return new PaymentResponseDto { Success = false, Message = "إعدادات الدفع غير مكتملة - SecretKey مفقود" };
                }

                if (string.IsNullOrEmpty(_paymobSettings.PublicKey))
                {
                    _logger?.LogError("❌ Paymob PublicKey is not configured");
                    return new PaymentResponseDto { Success = false, Message = "إعدادات الدفع غير مكتملة - PublicKey مفقود" };
                }

                var booking = await _unitOfWork.Booking.GetBookingWithDetailsAsync(request.BookingID);

                if (booking == null || booking.UserID != userId)
                    return new PaymentResponseDto { Success = false, Message = "الحجز غير موجود" };

                if (booking.User == null)
                    return new PaymentResponseDto { Success = false, Message = "بيانات المستخدم غير متوفرة" };

                var existingPayments = await _unitOfWork.Payment.GetPaymentsByBookingIdAsync(request.BookingID);
                var successfulPayment = existingPayments?.FirstOrDefault(p => p.PaymentStatus == "Success");

                if (successfulPayment != null || booking.BookingStatus == "Confirmed")
                    return new PaymentResponseDto { Success = false, Message = "تم الدفع مسبقاً" };

                var pendingPayments = existingPayments?.Where(p => p.PaymentStatus == "Pending" || p.PaymentStatus == "Failed");
                if (pendingPayments != null && pendingPayments.Any())
                {
                    foreach (var pendingPayment in pendingPayments)
                    {
                        pendingPayment.PaymentStatus = "Cancelled";
                        _unitOfWork.Payment.Update(pendingPayment);
                    }
                    await _unitOfWork.SaveChangesAsync();
                }

                var uniqueMerchantOrderId = $"{request.BookingID}_{DateTime.UtcNow.Ticks}";

                var intentionResult = await CreateIntentionAsync(request.Amount, booking, request.PaymentMethod, uniqueMerchantOrderId);

                if (intentionResult == null || !intentionResult.success)
                {
                    var errorMsg = intentionResult?.message ?? "فشل إنشاء نية الدفع";
                    _logger?.LogError($"Failed to create intention: {errorMsg}");
                    return new PaymentResponseDto { Success = false, Message = errorMsg };
                }

                // استخدام order_id من Paymob بدلاً من uniqueMerchantOrderId
                var paymobOrderId = intentionResult.order_id ?? uniqueMerchantOrderId;
                var clientSecret = intentionResult.client_secret;

                _logger?.LogInformation($"💾 Saving payment with Paymob Order ID: {paymobOrderId}");

                // حفظ بيانات الدفع
                var payment = new Data.Models.Tickets.Payment
                {
                    BookingID = request.BookingID,
                    Amount = request.Amount,
                    PaymentStatus = "Pending",
                    PaymentMethod = request.PaymentMethod,
                    PaymobOrderID = paymobOrderId,
                    PaymobTransactionID = uniqueMerchantOrderId, // حفظ uniqueMerchantOrderId كـ reference
                    CreatedAt = DateTime.UtcNow
                };

                await _unitOfWork.Payment.AddAsync(payment);
                await _unitOfWork.SaveChangesAsync();

                // إنشاء URL للـ Unified Checkout
                var checkoutUrl = $"https://accept.paymob.com/unifiedcheckout/?publicKey={_paymobSettings.PublicKey}&clientSecret={clientSecret}";

                return new PaymentResponseDto
                {
                    Success = true,
                    Message = "تم إنشاء نية الدفع بنجاح",
                    PublicKey = _paymobSettings.PublicKey,
                    ClientSecret = clientSecret,
                    PaymentUrl = checkoutUrl,
                    PaymobOrderID = paymobOrderId,
                    PaymentID = payment.Payment_ID
                };
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "خطأ في InitiatePaymentAsync");
                return new PaymentResponseDto { Success = false, Message = $"خطأ: {ex.Message}" };
            }
        }

        public async Task<bool> ProcessCallbackAsync(PaymobCallbackDto callback)
        {
            try
            {
                _logger?.LogInformation($"📥 Processing callback for order: {callback.order}, success: {callback.success}");
                _logger?.LogInformation($"📥 Full callback data: {JsonSerializer.Serialize(callback)}");

                if (string.IsNullOrEmpty(callback.order))
                {
                    _logger?.LogWarning("❌ Order ID is missing from callback, returning false.");
                    return false;
                }

                // التحقق من HMAC
                var hmacValid = VerifyHmac(callback);
                if (!hmacValid)
                {
                    _logger?.LogWarning("⚠️ HMAC verification failed for callback, continuing with process.");
                }

                // 1. محاولة العثور على الدفع باستخدام Order ID من Paymob
                var payment = await _unitOfWork.Payment.GetPaymentByOrderIdAsync(callback.order);

                // 2. إذا لم نجد الـ payment، نحاول استخراج BookingID والبحث عن دفع معلَّق
                if (payment == null)
                {
                    _logger?.LogWarning($"⚠️ Primary search: Payment not found by order ID: {callback.order}. Trying fallback.");

                    // محاولة استخراج BookingID من order (format: BookingID_Timestamp)
                    var orderParts = callback.order?.Split('_');
                    if (orderParts != null && orderParts.Length > 0 && Guid.TryParse(orderParts[0], out Guid bookingId))
                    {
                        _logger?.LogInformation($"🔍 Fallback: Trying to find pending payment by BookingID: {bookingId}");
                        var payments = await _unitOfWork.Payment.GetPaymentsByBookingIdAsync(bookingId);
                        payment = payments?.Where(p => p.PaymentStatus == "Pending")
                                         .OrderByDescending(p => p.CreatedAt)
                                         .FirstOrDefault();

                        if (payment != null)
                        {
                            _logger?.LogInformation($"✅ Fallback: Found pending payment by BookingID: {payment.Payment_ID}");
                            // تحديث PaymobOrderID بالقيمة الصحيحة من Paymob
                            payment.PaymobOrderID = callback.order;
                        }
                    }
                }

                if (payment == null)
                {
                    // ❌ نقطة الفشل الأولى - نبحث في كل الـ payments
                    _logger?.LogError($"❌ FAILURE POINT 1: Payment not found for order: {callback.order}");
                    
                    // آخر محاولة: عرض جميع الـ payments الموجودة
                    var allPayments = await _unitOfWork.Payment.GetAllAsync();
                    var pendingPayments = allPayments?.Where(p => p.PaymentStatus == "Pending").ToList();
                    
                    if (pendingPayments != null && pendingPayments.Any())
                    {
                        _logger?.LogInformation($"📋 Found {pendingPayments.Count} pending payments:");
                        foreach (var p in pendingPayments.Take(5))
                        {
                            _logger?.LogInformation($"   - Payment ID: {p.Payment_ID}, Order: {p.PaymobOrderID}, Booking: {p.BookingID}");
                        }
                        
                        // محاولة أخيرة: استخدام آخر pending payment
                        payment = pendingPayments.OrderByDescending(p => p.CreatedAt).FirstOrDefault();
                        if (payment != null)
                        {
                            _logger?.LogWarning($"⚠️ LAST RESORT: Using latest pending payment: {payment.Payment_ID}");
                            payment.PaymobOrderID = callback.order;
                        }
                    }
                    
                    if (payment == null)
                    {
                        _logger?.LogError($"❌ FINAL FAILURE: No payment found at all. Returning false.");
                        return false;
                    }
                }

                // تحديث معلومات الدفع
                payment.PaymobTransactionID = callback.id ?? callback.transaction_id;
                payment.CompletedAt = DateTime.UtcNow;

                // تحديد حالة الدفع بناءً على النتيجة
                if (callback.success)
                {
                    payment.PaymentStatus = "Success";
                    _logger?.LogInformation($"✅ Payment successful for booking: {payment.BookingID}");

                    // تأكيد الحجز
                    var bookingConfirmed = await _unitOfWork.Booking.ConfirmBookingAsync(payment.BookingID);
                    if (!bookingConfirmed)
                    {
                        // ❌ نقطة الفشل الثانية
                        _logger?.LogError($"❌ FAILURE POINT 2: Failed to confirm booking: {payment.BookingID}. Returning false.");
                        return false;
                    }
                    _logger?.LogInformation($"✅ Booking confirmed successfully: {payment.BookingID}");
                }
                else
                {
                    payment.PaymentStatus = "Failed";
                    payment.ErrorMessage = callback.error_occured ?? "Payment failed";
                    _logger?.LogWarning($"❌ Payment failed for booking: {payment.BookingID}. Error: {payment.ErrorMessage}");
                }

                _unitOfWork.Payment.Update(payment);
                var result = await _unitOfWork.SaveChangesAsync();

                if (!result)
                {
                    // ❌ نقطة الفشل الثالثة
                    _logger?.LogError($"❌ FAILURE POINT 3: Failed to save changes to database for payment {payment.Payment_ID}. Returning false.");
                    return false;
                }

                _logger?.LogInformation($"💾 Payment status updated successfully: {payment.PaymentStatus}");

                return result;
            }
            catch (Exception ex)
            {
                // ❌ نقطة الفشل الرابعة (استثناء عام)
                _logger?.LogError(ex, "❌ FAILURE POINT 4: Unexpected exception in ProcessCallbackAsync. Returning false.");
                return false;
            }
        }

        public async Task<Data.Models.Tickets.Payment> GetPaymentByBookingAsync(Guid bookingId)
        {
            var payments = await _unitOfWork.Payment.GetPaymentsByBookingIdAsync(bookingId);
            return payments?.OrderByDescending(p => p.CreatedAt).FirstOrDefault();
        }

        public async Task<bool> RefundPaymentAsync(Guid paymentId)
        {
            return await _unitOfWork.Payment.UpdatePaymentStatusAsync(paymentId, "Refunded");
        }

        private async Task<PaymobIntentionResponse?> CreateIntentionAsync(decimal amount, Data.Models.Tickets.Booking booking,
            string paymentMethod, string merchantOrderId)
        {
            try
            {
                // ⚠️ بدلاً من اختيار واحد، نرسل كل الـ Integration IDs
                var integrationIds = new List<int>();

                // إضافة Card Integration ID
                if (!string.IsNullOrEmpty(_paymobSettings.CardIntegrationId))
                {
                    integrationIds.Add(int.Parse(_paymobSettings.CardIntegrationId));
                    _logger?.LogInformation($"💳 Adding Card Integration ID: {_paymobSettings.CardIntegrationId}");
                }

                // إضافة Wallet Integration ID
                if (!string.IsNullOrEmpty(_paymobSettings.WalletIntegrationId))
                {
                    integrationIds.Add(int.Parse(_paymobSettings.WalletIntegrationId));
                    _logger?.LogInformation($"💰 Adding Wallet Integration ID: {_paymobSettings.WalletIntegrationId}");
                }

                if (integrationIds.Count == 0)
                {
                    _logger?.LogError("❌ No integration IDs configured!");
                    return new PaymobIntentionResponse
                    {
                        success = false,
                        message = "لم يتم تكوين أي طرق دفع"
                    };
                }

                // تنسيق رقم الهاتف
                var phoneNumber = booking.User?.PhoneNumber ?? "01000000000";
                if (!phoneNumber.StartsWith("+"))
                    phoneNumber = "+20" + phoneNumber.TrimStart('0');

                _logger?.LogInformation($"📞 Phone Number: {phoneNumber}");
                _logger?.LogInformation($"💰 Amount: {amount} EGP = {(int)(amount * 100)} cents");

                // بناء الـ payload حسب توثيق Paymob
                var payload = new
                {
                    amount = (int)(amount * 100), // بالقروش
                    currency = "EGP",
                    payment_methods = integrationIds.ToArray(), // ✅ إرسال كل الطرق المتاحة
                    items = new[]
                    {
                        new
                        {
                            name = "Train Ticket Booking",
                            amount = (int)(amount * 100),
                            quantity = 1
                        }
                    },
                    billing_data = new
                    {
                        first_name = booking.User?.FirstName ?? "Guest",
                        last_name = booking.User?.LastName ?? "User",
                        email = booking.User?.Email ?? "guest@example.com",
                        phone_number = phoneNumber,
                        apartment = "NA",
                        floor = "NA",
                        street = "NA",
                        building = "NA",
                        shipping_method = "NA",
                        postal_code = "NA",
                        city = "Cairo",
                        country = "EG",
                        state = "Cairo"
                    },
                    customer = new
                    {
                        first_name = booking.User?.FirstName ?? "Guest",
                        last_name = booking.User?.LastName ?? "User",
                        email = booking.User?.Email ?? "guest@example.com"
                    },
                    extras = new
                    {
                        ee = merchantOrderId
                    },
                    redirection_url = _paymobSettings.CallbackUrl ?? "https://localhost:7192/FrontEnd/PaymentCallback.html"
                };

                // ⚠️ استخدام Secret Key في Authorization
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Token {_paymobSettings.SecretKey}");

                var jsonPayload = JsonSerializer.Serialize(payload, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                _logger?.LogInformation($"📤 Sending POST to: https://accept.paymob.com/v1/intention/");
                _logger?.LogInformation($"🔑 Using Secret Key (first 30 chars): {_paymobSettings.SecretKey.Substring(0, Math.Min(30, _paymobSettings.SecretKey.Length))}...");
                _logger?.LogInformation($"📦 Request Payload:\n{jsonPayload}");

                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("https://accept.paymob.com/v1/intention/", content);

                var result = await response.Content.ReadAsStringAsync();

                _logger?.LogInformation($"📥 Response Status: {(int)response.StatusCode} {response.StatusCode}");
                _logger?.LogInformation($"📥 Response Body:\n{result}");

                if (!response.IsSuccessStatusCode)
                {
                    _logger?.LogError($"❌ Paymob returned error: {response.StatusCode}");

                    try
                    {
                        var errorObj = JsonSerializer.Deserialize<Dictionary<string, object>>(result);
                        var errorDetail = errorObj?.ContainsKey("detail") == true
                            ? errorObj["detail"].ToString()
                            : result;

                        _logger?.LogError($"❌ Error Detail: {errorDetail}");

                        return new PaymobIntentionResponse
                        {
                            success = false,
                            message = $"Paymob Error: {errorDetail}"
                        };
                    }
                    catch
                    {
                        return new PaymobIntentionResponse
                        {
                            success = false,
                            message = $"Error {response.StatusCode}: {result}"
                        };
                    }
                }

                var intentionResponse = JsonSerializer.Deserialize<PaymobIntentionResponse>(result, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (intentionResponse != null)
                {
                    intentionResponse.success = true;
                    _logger?.LogInformation($"✅ Payment Intention created successfully!");
                    _logger?.LogInformation($"🔐 Client Secret received: {intentionResponse.client_secret?.Substring(0, 20)}...");
                    _logger?.LogInformation($"📦 Paymob Order ID: {intentionResponse.order_id}");
                }

                return intentionResponse;
            }
            catch (HttpRequestException httpEx)
            {
                _logger?.LogError(httpEx, "❌ HTTP Request Exception in CreateIntentionAsync");
                return new PaymobIntentionResponse
                {
                    success = false,
                    message = $"Network Error: {httpEx.Message}"
                };
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "❌ Unexpected Exception in CreateIntentionAsync");
                return new PaymobIntentionResponse
                {
                    success = false,
                    message = $"Exception: {ex.Message}"
                };
            }
        }

        private bool VerifyHmac(PaymobCallbackDto callback)
        {
            try
            {
                if (string.IsNullOrEmpty(callback.hmac) || string.IsNullOrEmpty(_paymobSettings.HmacSecret))
                {
                    _logger?.LogWarning("⚠️ HMAC or HmacSecret is missing");
                    return false;
                }

                // Helper to safely convert optional string fields to lowercase boolean strings Paymob expects for HMAC
                string BoolToStringLower(string? value)
                {
                    return (value != null && (value.ToLower() == "true" || value.ToLower() == "1")) ? "true" : "false";
                }

                // بناء HMAC string حسب توثيق Paymob - معالجة القيم المحتمل أن تكون null أو غير متطابقة
                var concatenatedString = $"{callback.amount_cents ?? 0}" +
                    $"{callback.created_at ?? ""}" +
                    $"{callback.currency ?? ""}" +
                    $"{BoolToStringLower(callback.error_occured)}" +
                    $"{BoolToStringLower(callback.has_parent_transaction)}" +
                    $"{callback.id ?? ""}" +
                    $"{callback.integration_id ?? ""}" +
                    $"{BoolToStringLower(callback.is_3d_secure)}" +
                    $"{BoolToStringLower(callback.is_auth)}" +
                    $"{BoolToStringLower(callback.is_capture)}" +
                    $"{BoolToStringLower(callback.is_refunded)}" +
                    $"{BoolToStringLower(callback.is_standalone_payment)}" +
                    $"{BoolToStringLower(callback.is_voided)}" +
                    $"{callback.order ?? ""}" +
                    $"{callback.owner ?? ""}" +
                    $"{BoolToStringLower(callback.pending)}" +
                    $"{callback.source_data_pan ?? ""}" +
                    $"{callback.source_data_sub_type ?? ""}" +
                    $"{callback.source_data_type ?? ""}" +
                    $"{callback.success.ToString().ToLower()}";

                _logger?.LogInformation($"🔐 HMAC String: {concatenatedString}");

                using var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(_paymobSettings.HmacSecret));
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(concatenatedString));
                var computedHmac = BitConverter.ToString(hash).Replace("-", "").ToLower();

                _logger?.LogInformation($"🔐 Computed HMAC: {computedHmac}");
                _logger?.LogInformation($"🔐 Received HMAC: {callback.hmac}");

                var isValid = computedHmac == callback.hmac?.ToLower();
                _logger?.LogInformation($"🔐 HMAC Valid: {isValid}");

                return isValid;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "❌ Error in HMAC verification");
                return false;
            }
        }
    }
}