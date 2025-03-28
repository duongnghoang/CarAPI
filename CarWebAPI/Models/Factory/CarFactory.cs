using CarWebAPI.Contracts;

namespace CarWebAPI.Models.Factory;

internal sealed class CarFactory : ICarFactory
{
    public Car? CreateCar(int id, string make, string model, int year, DateTime lastMaintenanceTime, string carType)
    {
        if (carType == "Fuel")
        {
            var fuelCar = new FuelCar(id, make, model, year, lastMaintenanceTime);
            fuelCar.ScheduleMaintenance();
            return fuelCar;
        }

        if (carType == "Electric")
        {
            var electricCar = new ElectricCar(id, make, model, year, lastMaintenanceTime);
            electricCar.ScheduleMaintenance();
            return electricCar;
        }

        return null;
    }
}