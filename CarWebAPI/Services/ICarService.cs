using CarWebAPI.Common;
using CarWebAPI.Contracts;
using CarWebAPI.Models;

namespace CarWebAPI.Services
{
    public interface ICarService
    {
        Result<IEnumerable<Car>> GetAllCars();
        Result<Car> AddCar(AddCarRequest request);
        Result<Car> GetCarById(int id);
        Result<UpdateMaintenanceResponse> UpdateCar(int id, DateTime newMaintenanceDate);
    }
}