using Domain.Dtos;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrainController : ControllerBase
    {
        private readonly ITrainService _trainService;

        public TrainController(ITrainService trainService)
        {
            _trainService = trainService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var trains = await _trainService.GetAllAsync();
            return Ok(trains);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var train = await _trainService.GetByIdAsync(id);
            if (train == null)
                return NotFound("Train not found");

            return Ok(train);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] TrainDto train)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _trainService.AddAsync(train);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TrainDto dto)
        {
            if (id != dto.Train_ID)
                return BadRequest("Invalid train ID");

            var result = await _trainService.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _trainService.DeleteAsync(id);
            return Ok(result);
        }
    }
}