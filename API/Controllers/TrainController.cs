using Domain.Dtos;

using Domain.Dtos.TrainDtos;
using Domain.Services.TrainService;
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
        // GET: api/Train
        [HttpGet]
        public async Task<IActionResult> GetAllTrains()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var trains = await _trainService.GetAllTrainsAsync();
            return Ok(trains);
        }

        // GET: api/Train/100
        [HttpGet("id/{trainId}")]
        public async Task<IActionResult> GetTrainById(string trainId)
        {
            var train = await _trainService.GetTrainByIdAsync(trainId);
            if (train == null)
            {
                return NotFound($"لم يتم العثور على قطار برقم {trainId}");
            }
            return Ok(train);
        }
        [HttpGet("name/{trainname}")]
        public async Task<IActionResult> GetTrainByName(string trainname)
        {
            var train = await _trainService.GetTrainByNameAsync(trainname);
            if (train == null)
            {
                return NotFound($"لم يتم العثور على قطار برقم {trainname}");
            }
            return Ok(train);
        }

        // POST: api/Train
        [HttpPost]
        public async Task<IActionResult> CreateTrain([FromBody] TrainCreateDto trainDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdTrain = await _trainService.CreateTrainAsync(trainDto);
                return CreatedAtAction(nameof(GetTrainById), new { trainId = createdTrain.Train_ID }, createdTrain);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/Train/100
        [HttpPut("{trainId}")]
        public async Task<IActionResult> UpdateTrain(string trainName, [FromBody] TrainCreateDto trainDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = await _trainService.UpdateTrainAsync(trainName, trainDto);
            if (!success)
            {
                return NotFound($"لم يتم العثور على قطار برقم {trainName} للتحديث.");
            }

            return Ok(new { message = $"تم تحديث القطار '{trainName}' بنجاح." });
        }

        // DELETE: api/Train/100
        [HttpDelete("{trainId}")]
        public async Task<IActionResult> DeleteTrain(long trainId)
        {
            var success = await _trainService.DeleteTrainAsync(trainId);
            if (!success)
            {
                return NotFound($"لم يتم العثور على قطار برقم {trainId} للحذف.");
            }

            return Ok(new { message = $"تم حذف القطار بنجاح" });
        }

    }
}