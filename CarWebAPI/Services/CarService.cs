using CarWebAPI.Common;
using CarWebAPI.Contracts;
using CarWebAPI.DbContext;
using CarWebAPI.Models;
using CarWebAPI.Models.Factory;

namespace CarWebAPI.Services;

public class CarService : ICarService
{
    private readonly ICarFactory _carFactory;
    private readonly IInMemoryDbContext _dbContext;

    public CarService(IInMemoryDbContext dbContext, ICarFactory carFactory)
    {
        _dbContext = dbContext;
        _carFactory = carFactory;
    }

    public Result<Car> AddCar(AddCarRequest carRequest)
    {
        var car = _carFactory.CreateCar(carRequest.Id, carRequest.Make, carRequest.Model, carRequest.Year,
            carRequest.LastMaintanenceTime, carRequest.CarType);
        if (car != null)
        {
            _dbContext.Add(car);
            return Result<Car>.Success(car);
        }

        return Result<Car>.Fail("Car not created");
    }

    public Result<IEnumerable<Car>> GetAllCars()
    {
        var cars = _dbContext.GetAll();
        return Result<IEnumerable<Car>>.Success(cars);
    }

    public Result<Car> GetCarById(int id)
    {
        var car = _dbContext.GetById(id);
        if (car != null) return Result<Car>.Success(car);
        return Result<Car>.Fail("Car not found");
    }

    public Result<UpdateMaintenanceResponse> UpdateCar(int id, DateTime newMaintenanceDate)
    {
        var car = _dbContext.GetById(id);
        if (car != null) return Result<UpdateMaintenanceResponse>.Fail("Car not found");
        var updatedCar = _dbContext.Update(id, newMaintenanceDate);
        return Result<UpdateMaintenanceResponse>.Success(new UpdateMaintenanceResponse(updatedCar!.Id, updatedCar.Model,
            updatedCar.NextMaintenanceDate));
    }
}