using CarWebAPI.CarManagement;
using Microsoft.AspNetCore.Mvc;
using Middleware;

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
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CarController>
        [MiddlewareFilter(typeof(InputValidateMiddleware))]
        [HttpPost]
        public IActionResult AddCar([FromBody] CarRequest car)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            if (car.CarType == "Fuel")
            {
                var fuelCar = new FuelCar(car.Id, car.Make, car.Model, car.Year, car.LastMaintanenceTime);
                _carManager.AddCar(fuelCar);
                return Ok(fuelCar);
            }
            else if (car.CarType == "Electric")
            {
                var electricCar = new ElectricCar(car.Id, car.Make, car.Model, car.Year, car.LastMaintanenceTime);
                _carManager.AddCar(electricCar);
                return Ok(electricCar);
            }
            return BadRequest();
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

    public record CarRequest(int Id, string Make, string Model, int Year, string CarType, DateTime LastMaintanenceTime);
}
