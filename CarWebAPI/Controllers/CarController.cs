using CarWebAPI.DbContext;
using CarWebAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarManager _carManager;
        public CarController(ICarManager carManager)
        {
            _carManager = carManager;
        }

        // GET: api/<CarController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CarController>/5
        [HttpGet("view-all")] // GET api/cars/view-all
        public IActionResult ViewAllCars()
        {
            var cars = _carManager.GetAllCars();
            if (!cars.Any())
                return NotFound("No cars found.");

            return Ok(cars);
        }

        [HttpGet("maintenance/{id}")] // GET api/cars/last-maintenance/{id}
        public IActionResult GetLastMaintenanceTime(int id)
        {
            var car = _carManager.GetById(id);
            if (car == null)
                return NotFound("Car not found.");

            return Ok(car.LastMaintenanceDate);
        }

        // POST api/<CarController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CarController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CarController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
