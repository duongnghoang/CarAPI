using CarWebAPI.Contracts;
using CarWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet("view-all")]
        public IActionResult ViewAllCars()
        {
            var response = _carService.GetAllCars();
            return Ok(response);
        }

        [HttpGet("view-detail/{id}")]
        public IActionResult GetCarById(int id)
        {
            var car = _carService.GetCarById(id);
            return Ok(car);
        }

        // POST api/<CarController>
        [HttpPost("add-car")]
        public IActionResult AddCar([FromBody] AddCarRequest request)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            var response = _carService.AddCar(request);
            return Ok(response);
        }

        [HttpPut("update-car-maintenance/{id}")]
        public IActionResult UpdateMaintenanceDate(int id, [FromBody] UpdateMaintenanceRequest request)
        {
            var response = _carService.UpdateCar(id, request.NewMaintenanceDate);
            return Ok(response);
        }
    }
}
