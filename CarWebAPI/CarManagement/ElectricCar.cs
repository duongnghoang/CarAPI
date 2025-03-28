namespace NashTechAssignment
{
    public class ElectricCar : Car, IChargable, IShowRefill
    {
        public DateTime chargeTime;
        public ElectricCar(string make, string model, int year, DateTime lastMaintenanceDate)
            : base(make, model, year, lastMaintenanceDate)
        {
        }
        public override void DisplayDetails()
        {
            Console.WriteLine($"Car: {make}{model} ({year})\n Type: Electric Car\n Last Maintenance Date: {lastMaintenanceDate}\n Next Maintenance Date: {nextMaintenanceDate}");
        }

        public void Charge(DateTime timeOfCharge)
        {
            chargeTime = timeOfCharge;
            ShowRefillTime();
        }

        public DateTime ShowLastChargeTime()
        {
            return chargeTime;
        }

        public void ShowRefillTime()
        {
            Console.WriteLine($"ElectricCar {make} {model} refueled on {chargeTime}");
        }
    }

}