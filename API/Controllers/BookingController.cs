using Domain.Dtos;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingDto dto)
        {
            var booking = await _bookingService.CreateBookingAsync(dto);
            return Ok(booking);
        }
    }
 }

