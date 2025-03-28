using CarWebAPI.Contracts;

namespace CarWebAPI.Models.Factory;

internal sealed class CarFactory : ICarFactory
{
    public Car? CreateCar(int id, string make, string model, int year, DateTime lastMaintenanceTime, string carType)
    {
        if (carType == "Fuel")
        {
            return new FuelCar(id, make, model, year, lastMaintenanceTime);
        }

        if (carType == "Electric")
        {
            return new ElectricCar(id, make, model, year, lastMaintenanceTime);
        }

        return null;
    }
}