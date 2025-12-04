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
                var userId = GetCurrentUserId();
                var result = await _bookingService.CreateBookingAsync(userId, dto);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
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

        [HttpDelete("{bookingId}/cancel")]
        public async Task<ActionResult> CancelBooking(Guid bookingId, [FromBody] string reason = null)
        {
            try
            {
                var userId = GetCurrentUserId();
                
                var cancelDto = new BookingCancelDto
                {
                    BookingId = bookingId,
                    Reason = reason ?? "إلغاء من قبل المستخدم"
                };
                
                var result = await _bookingService.CancelBookingAsync(cancelDto);
                
                if (result)
                    return Ok(new { message = "تم إلغاء الحجز بنجاح" });
                
                return BadRequest(new { message = "فشل إلغاء الحجز" });
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "حدث خطأ أثناء إلغاء الحجز", details = ex.Message });
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