using CarWebAPI.CarManagement;
using Microsoft.AspNetCore.Mvc;
using Middleware;
using ICarManager = CarWebAPI.Interfaces.ICarManager;

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

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("view-all")]
        public IActionResult ViewAllCars()
        {
            var cars = _carManager.GetAllCars();
            if (!cars.Any())
                return NotFound("No cars found.");

            return Ok(cars);
        }

        [HttpGet("maintenance/{id}")]
        public IActionResult GetLastMaintenanceTime(int id)
        {
            var car = _carManager.GetById(id);
            if (car == null)
                return NotFound("Car not found.");

            return Ok(car.LastMaintenanceDate);
        }

        // POST api/<CarController>
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

        // DTO class
        public class MaintenanceUpdateRequest
        {
            public DateTime NewMaintenanceDate { get; set; }
        }
        [HttpPut("update-maintenance/{id}")]
        public IActionResult UpdateMaintenanceDate(int id, [FromBody] MaintenanceUpdateRequest request)
        {
            var car = _carManager.GetById(id);
            if (car == null)
                return NotFound("Car not found.");

            _carManager.UpdateCar(id, request.NewMaintenanceDate);

            return Ok(new { car.Id, car.Model, UpdatedNextMaintenanceDate = car.NextMaintenanceDate });
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
