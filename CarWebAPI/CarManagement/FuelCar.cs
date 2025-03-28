
namespace NashTechAssignment
{
    public class FuelCar : Car, IFuelable, IShowRefill
    {

        public DateTime refuelTime;
        public FuelCar(string make, string model, int year, DateTime lastMaintenanceDate)
            : base(make, model, year, lastMaintenanceDate)
        {
        }
        public override void DisplayDetails()
        {
            Console.WriteLine($"Car: {make}{model} ({year})\n Type: Fuel Car\n Last Maintenance Date: {lastMaintenanceDate}\n Next Maintenance Date: {nextMaintenanceDate}");
        }

        public void Refuel(DateTime timeOfRefuel)
        {

            refuelTime = timeOfRefuel;
            ShowRefillTime();
        }

        public DateTime ShowLastFuelTime()
        {
            return refuelTime;
        }

        public void ShowRefillTime()
        {
            Console.WriteLine($"FuelCar {make} {model} refueled on {refuelTime}");
        }
    }
}