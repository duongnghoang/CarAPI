namespace CarWebAPI.Models.Factory;

public interface ICarFactory
{
    Car? CreateCar(int id, string make, string model, int year, DateTime lastMaintenanceTime, string carType);
}