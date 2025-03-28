using CarWebAPI.Interfaces;

namespace CarWebAPI.Models;

public class FuelCar : Car, IFuelable, IShowRefill
{
    public DateTime RefuelTime;

    public FuelCar(int id, string make, string model, int year, DateTime lastMaintenanceDate)
        : base(id, make, model, year, lastMaintenanceDate)
    {
    }

    public void Refuel(DateTime timeOfRefuel)
    {
        RefuelTime = timeOfRefuel;
        ShowRefillTime();
    }

    public DateTime ShowLastFuelTime()
    {
        return RefuelTime;
    }

    public void ShowRefillTime()
    {
        Console.WriteLine($"FuelCar {Make} {Model} refueled on {RefuelTime}");
    }

    public override void DisplayDetails()
    {
        Console.WriteLine(
            $"Car: {Make}{Model} ({Year})\n Type: Fuel Car\n Last Maintenance Date: {LastMaintenanceDate}\n Next Maintenance Date: {NextMaintenanceDate}");
    }
}