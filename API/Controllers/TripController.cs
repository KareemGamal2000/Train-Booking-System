using Domain.Dtos.TripDtos;
using Domain.Services.TripService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripController : ControllerBase
    {
        private readonly ITripService _tripService;

        public TripController(ITripService tripService)
        {
            _tripService = tripService;
        }

        // GET: api/Trip?pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAllTrips(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1 || pageSize > 50) pageSize = 10; 

            var pagedResult = await _tripService.GetAllTripsAsync(pageNumber, pageSize);

            Response.Headers.Add("X-Total-Count", pagedResult.TotalCount.ToString());
            Response.Headers.Add("X-Total-Pages", pagedResult.TotalPages.ToString());
            Response.Headers.Add("X-Page-Number", pagedResult.PageNumber.ToString());
            Response.Headers.Add("X-Page-Size", pagedResult.PageSize.ToString());

            return Ok(pagedResult);
        }

        // GET: api/Trip/details/101
        [HttpGet("details/{tripId}")]
        public async Task<IActionResult> GetTripDetails(int tripId)
        {
            var trip = await _tripService.GetTripDetailsAsync(tripId);
            if (trip == null)
            {
                return NotFound($"لم يتم العثور على رحلة بتفاصيل كاملة بالرقم {tripId}");
            }
            return Ok(trip);
        }

        // GET: api/Trip/search/station?stationName=أسيوط
        [HttpGet("search/station")]
        public async Task<IActionResult> GetTripsByStation([FromQuery] string stationName)
        {
            if (string.IsNullOrEmpty(stationName))
            {
                return BadRequest("اسم المحطة مطلوب.");
            }
            var trips = await _tripService.GetTripsByStationAsync(stationName);
            return Ok(trips);
        }

        // GET: api/Trip/search/route?departureStationName=القاهرة&arrivalStationName=أسوان
        [HttpGet("search/alltrips")]
        public async Task<IActionResult> FindTripsWithTwoStations(
            [FromQuery] string departureStationName,
            [FromQuery] string arrivalStationName)
        {
            if (string.IsNullOrEmpty(departureStationName) || string.IsNullOrEmpty(arrivalStationName))
            {
                return BadRequest("يجب تحديد محطتي المغادرة والوصول.");
            }

            var trips = await _tripService.FindTripsWithTwoStationsAsync(departureStationName, arrivalStationName);
            return Ok(trips);
        }

        // POST: api/Trip
        [HttpPost]
        public async Task<IActionResult> CreateTrip([FromBody] TripCreateDto tripDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdTrip = await _tripService.CreateTripAsync(tripDto);
            return CreatedAtAction(nameof(GetTripDetails), new { tripId = createdTrip.Trip_ID }, createdTrip);
        }

        // DELETE: api/Trip/1
        [HttpDelete("{tripId}")]
        public async Task<IActionResult> DeleteTrip(int tripId)
        {
            var success = await _tripService.DeleteTripAsync(tripId);
            if (!success)
            {
                return NotFound($"لم يتم العثور على رحلة برقم {tripId} للحذف.");
            }

            return NoContent();
        }
    }
}
