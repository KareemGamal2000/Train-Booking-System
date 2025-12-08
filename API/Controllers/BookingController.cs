using Domain.Dtos.BookingDto;
using Domain.Dtos.BookingDtos;
using Domain.Services.BookingService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

      
        [HttpPost("create")]
        public async Task<ActionResult<BookingConfirmationDto>> CreateBooking([FromBody] BookingCreateDto dto)
        {
            try
            {
                // Logging للتشخيص
                Console.WriteLine("=== Booking Create Request ===");
                Console.WriteLine($"TripID: {dto.TripID}");
                Console.WriteLine($"ClassID: {dto.ClassID}");
                Console.WriteLine($"DepartureStopID: {dto.DepartureStopID}");
                Console.WriteLine($"ArrivalStopID: {dto.ArrivalStopID}");
                Console.WriteLine($"NumberOfSeats: {dto.NumberOfSeats}");
                Console.WriteLine($"SelectedSeatIDs: {(dto.SelectedSeatIDs != null ? string.Join(", ", dto.SelectedSeatIDs) : "null")}");
                
                var userId = GetCurrentUserId();
                Console.WriteLine($"UserID: {userId}");
                
                var result = await _bookingService.CreateBookingAsync(userId, dto);
                Console.WriteLine("Booking created successfully");
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"InvalidOperationException: {ex.Message}");
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                return StatusCode(500, new { message = "حدث خطأ أثناء إنشاء الحجز", details = ex.Message });
            }
        }

        [HttpGet("{bookingId}")]
        public async Task<ActionResult<BookingReadDto>> GetBookingDetails(Guid bookingId)
        {
            try
            {
                var result = await _bookingService.GetBookingByIdAsync(bookingId);
                
                if (result == null)
                    return NotFound(new { message = "الحجز غير موجود" });
                
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "حدث خطأ أثناء جلب تفاصيل الحجز", details = ex.Message });
            }
        }

        [HttpGet("user/bookings")]
        public async Task<ActionResult<IEnumerable<BookingReadDto>>> GetUserBookings()
        {
            try
            {
                var userId = GetCurrentUserId();
                var result = await _bookingService.GetUserBookingsAsync(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "حدث خطأ أثناء جلب حجوزات المستخدم", details = ex.Message });
            }
        }
        [HttpGet("available-seats")]
        [AllowAnonymous] // السماح بجلب المقاعد بدون تسجيل دخول
        public async Task<ActionResult<AvailableSeatsDto>> GetAvailableSeats(
            [FromQuery] int tripId,
            [FromQuery] long classId,
            [FromQuery] int departureStopId,
            [FromQuery] int arrivalStopId)
        {
            try
            {
                var result = await _bookingService.GetAvailableSeatsAsync(tripId, classId, departureStopId, arrivalStopId);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "حدث خطأ أثناء جلب المقاعد المتاحة", details = ex.Message });
            }
        }

        [HttpPost("{bookingId}/select-seats")]
        public async Task<ActionResult> SelectSeats(Guid bookingId, [FromBody] BookingSeatSelectionDto dto)
        {
            try
            {
                // التحقق من صحة البيانات
                if (dto == null)
                    return BadRequest(new { message = "بيانات الطلب مفقودة" });
                
                if (dto.SelectedSeatIDs == null || !dto.SelectedSeatIDs.Any())
                    return BadRequest(new { message = "يجب اختيار مقعد واحد على الأقل" });
                
                if (dto.PricePerSeat <= 0)
                    return BadRequest(new { message = "السعر غير صحيح" });
                
                var userId = GetCurrentUserId();
                var result = await _bookingService.SelectSeatsAsync(bookingId, dto);
                
                if (result)
                    return Ok(new { message = "تم اختيار المقاعد بنجاح" });
                
                return BadRequest(new { message = "فشل اختيار المقاعد" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "حدث خطأ أثناء اختيار المقاعد", details = ex.Message });
            }
        }

        [HttpPost("{bookingId}/confirm")]
        public async Task<ActionResult> ConfirmBooking(Guid bookingId)
        {
            try
            {
                var userId = GetCurrentUserId();
                var result = await _bookingService.ConfirmBookingAsync(bookingId);
                
                if (result)
                    return Ok(new { message = "تم تأكيد الحجز بنجاح" });
                
                return BadRequest(new { message = "فشل تأكيد الحجز" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "حدث خطأ أثناء تأكيد الحجز", details = ex.Message });
            }
        }

        [HttpPost("debug/generate-seats/{coachId}")]
        [AllowAnonymous]
        public async Task<ActionResult> GenerateSeatsForCoach(long coachId)
        {
            try
            {
                // للتشخيص فقط - يجب حذف هذا في الإنتاج
                var result = await _bookingService.GenerateSeatsForCoachAsync(coachId);
                return Ok(new { message = $"تم إنشاء {result} مقعد", seatsGenerated = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "حدث خطأ أثناء إنشاء المقاعد", details = ex.Message });
            }
        }

        [HttpDelete("{bookingId}/cancel")]
        public async Task<ActionResult> CancelBooking(Guid bookingId, [FromBody] BookingCancelDto? request = null)
        {
            try
            {
                var userId = GetCurrentUserId();
                Console.WriteLine($"[BookingController] User {userId} requesting to cancel booking {bookingId}");
                
                // التحقق من ملكية الحجز
                var booking = await _bookingService.GetBookingByIdAsync(bookingId);
                
                if (booking == null)
                {
                    return NotFound(new { message = "الحجز غير موجود" });
                }

                if (booking.UserID != userId)
                {
                    Console.WriteLine($"[BookingController] User {userId} is not the owner of booking {bookingId}");
                    return Forbid();
                }
                
                var cancelDto = new BookingCancelDto
                {
                    BookingId = bookingId,
                    Reason = request?.Reason ?? "إلغاء من قبل المستخدم"
                };
                
                var result = await _bookingService.CancelBookingAsync(cancelDto);
                
                if (result)
                {
                    Console.WriteLine($"[BookingController] Booking {bookingId} cancelled successfully");
                    return Ok(new 
                    { 
                        message = "تم إلغاء الحجز بنجاح",
                        bookingId = bookingId,
                        cancelledAt = DateTime.UtcNow
                    });
                }
                
                return BadRequest(new { message = "فشل إلغاء الحجز" });
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"[BookingController] Unauthorized: {ex.Message}");
                return Unauthorized(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"[BookingController] Invalid operation: {ex.Message}");
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[BookingController] ERROR: {ex.Message}");
                Console.WriteLine($"[BookingController] Stack trace: {ex.StackTrace}");
                return StatusCode(500, new 
                { 
                    message = "حدث خطأ أثناء إلغاء الحجز", 
                    details = ex.Message 
                });
            }
        }

        [HttpGet("{bookingId}/summary")]
        public async Task<ActionResult<BookingSummaryDto>> GetBookingSummary(Guid bookingId)
        {
            try
            {
                var result = await _bookingService.GetBookingSummaryAsync(bookingId);
                
                if (result == null)
                    return NotFound(new { message = "الحجز غير موجود" });
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "حدث خطأ أثناء جلب ملخص الحجز", details = ex.Message });
            }
        }

        private Guid GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst("uid")?.Value           
                           ?? User.FindFirst("sub")?.Value          
                           ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value; 

            if (string.IsNullOrEmpty(userIdClaim))
                throw new UnauthorizedAccessException("لم يتم العثور على معرف المستخدم في Token");

            if (!Guid.TryParse(userIdClaim, out var userId))
                throw new UnauthorizedAccessException($"معرف المستخدم غير صحيح: {userIdClaim}");

            return userId;
        }
    }
}