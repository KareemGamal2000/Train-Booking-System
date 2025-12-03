using Domain.Dtos.PaymentDtos;
using Domain.Services.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("initiate")]
        [Authorize]
        public async Task<IActionResult> InitiatePayment([FromBody] PaymentRequestDto request)
        {
            var userId = User.FindFirst("uid")?.Value ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { message = "المستخدم غير مصرح له" });

            var result = await _paymentService.InitiatePaymentAsync(request, Guid.Parse(userId));

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("callback")]
        [AllowAnonymous]
        public async Task<IActionResult> PaymobCallback([FromBody] PaymobCallbackDto callback)
        {
            var result = await _paymentService.ProcessCallbackAsync(callback);

            if (!result)
                return BadRequest(new { message = "فشل معالجة الاستجابة" });

            return Ok(new { message = "تم معالجة الدفع بنجاح" });
        }

        [HttpGet("booking/{bookingId}")]
        [Authorize]
        public async Task<IActionResult> GetPaymentByBooking(Guid bookingId)
        {
            var payment = await _paymentService.GetPaymentByBookingAsync(bookingId);

            if (payment == null)
                return NotFound(new { message = "لا يوجد دفع لهذا الحجز" });

            return Ok(payment);
        }

        [HttpPost("refund/{paymentId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RefundPayment(Guid paymentId)
        {
            var result = await _paymentService.RefundPaymentAsync(paymentId);

            if (!result)
                return BadRequest(new { message = "فشل استرجاع المبلغ" });

            return Ok(new { message = "تم استرجاع المبلغ بنجاح" });
        }
    }
}