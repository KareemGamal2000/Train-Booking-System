using Data.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using API.SeedData;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SeatController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("generate-all")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GenerateAllSeats()
        {
            try
            {
                await SeatGenerator.GenerateSeatsForCoachesAsync(_context);
                return Ok(new { message = "تم توليد المقاعد بنجاح" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "حدث خطأ أثناء توليد المقاعد", details = ex.Message });
            }
        }
    }
}