using Domain.Dtos;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoachController : ControllerBase
    {
        private readonly ICoachService _coachService;

        public CoachController(ICoachService coachService)
        {
            _coachService = coachService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var coaches = await _coachService.GetAllAsync();
            return Ok(coaches);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var coach = await _coachService.GetByIdAsync(id);
            if (coach == null)
                return NotFound("Coach not found");

            return Ok(coach);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CoachDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _coachService.AddAsync(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CoachDto dto)
        {
            if (id != dto.Coach_ID)
                return BadRequest("Invalid coach ID");

            var result = await _coachService.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _coachService.DeleteAsync(id);
            return Ok(result);
        }
    }
}