using Domain.Dtos.StationDtos;
using Domain.Services.StationService;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationController : ControllerBase
    {
        private readonly IStationService _stationService;

        public StationController(IStationService stationService)
        {
            _stationService = stationService;
        }

        // GET: api/Station
        [HttpGet]
        public async Task<IActionResult> GetAllStations()
        {
            var stations = await _stationService.GetAllStationAsync();
            return Ok(stations);
        }

        // GET: api/Station/name?stationname=القاهرة
        [HttpGet("name")]
        public async Task<IActionResult> GetStationByName([FromQuery] string stationname)
        {
            var station = await _stationService.GetStationBynameAsync(stationname);
            if (station == null)
            {
                return NotFound($"لم يتم العثور على محطة بالاسم {stationname}");
            }
            return Ok(station);
        }

        // GET: api/Station/1
        [HttpGet("{stationId}")]
        public async Task<IActionResult> GetStationById(long stationId)
        {
            var station = await _stationService.GetStationByIdAsync(stationId);
            if (station == null)
            {
                return NotFound($"لم يتم العثور على محطة برقم {stationId}");
            }
            return Ok(station);
        }

        // POST: api/Station
        [HttpPost("Create/")]
        public async Task<IActionResult> CreateStation([FromBody] StationCreateDto stationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdStation = await _stationService.CreateStationAsync(stationDto);
                return CreatedAtAction(nameof(GetStationById), new { stationId = createdStation.StationID }, createdStation);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/Station/Update/{stationname}
        [HttpPut("Update/{stationname}")]
        public async Task<IActionResult> UpdateStation(string stationname, [FromBody] StationUpdateDto stationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = await _stationService.UpdateStationAsync(stationname, stationDto);
            if (!success)
            {
                return NotFound($"لم يتم العثور على محطة باسم {stationname} للتحديث.");
            }

            return Ok(new { message = $"تم تحديث المحطة '{stationname}' بنجاح." });
        }

        // DELETE: api/Station/1
        [HttpDelete("Delete/{stationId}")]
        public async Task<IActionResult> DeleteStation(long stationId)
        {
            var success = await _stationService.DeleteStationAsync(stationId);
            if (!success)
            {
                return NotFound($"لم يتم العثور على محطة برقم {stationId} للحذف.");
            }

            return Ok(new { message = $"تم حذف المحطة بنجاح" });
        }
    }
}
