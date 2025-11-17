namespace API.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class RoutesController : ControllerBase
    {
        private static List<Route> _routes = new()
    {
        new Route { Id = 1, FromStation = "Cairo", ToStation = "Alexandria", DepartureTime = "08:00", ArrivalTime = "10:30", TrainId = 1, Price = 150 },
        new Route { Id = 2, FromStation = "Cairo", ToStation = "Luxor", DepartureTime = "07:00", ArrivalTime = "13:00", TrainId = 2, Price = 300 },
        new Route { Id = 3, FromStation = "Alexandria", ToStation = "Aswan", DepartureTime = "06:30", ArrivalTime = "15:45", TrainId = 3, Price = 400 }
    };

        [HttpGet]
        public IActionResult GetRoutes()
        {
            return Ok(_routes);
        }

        [HttpGet("{id}")]
        public IActionResult GetRoute(int id)
        {
            var route = _routes.FirstOrDefault(r => r.Id == id);
            if (route == null) return NotFound();
            return Ok(route);
        }

        [HttpPost]
        public IActionResult CreateRoute([FromBody] Route newRoute)
        {
            newRoute.Id = _routes.Count + 1;
            _routes.Add(newRoute);
            return CreatedAtAction(nameof(GetRoute), new { id = newRoute.Id }, newRoute);
        }
    }

    public class Route
    {
        public int Id { get; set; }
        public string FromStation { get; set; }
        public string ToStation { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
        public int TrainId { get; set; }
        public decimal Price { get; set; }
    }
}
