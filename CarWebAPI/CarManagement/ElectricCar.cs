using CarWebAPI.Interfaces;

namespace CarWebAPI.CarManagement
{
    public class ElectricCar : Car, IChargable, IShowRefill
    {
        public DateTime ChargeTime;
        public ElectricCar(string make, string model, int year, DateTime lastMaintenanceDate)
            : base(make, model, year, lastMaintenanceDate)
        {
        }
        public override void DisplayDetails()
        {
            Console.WriteLine($"Car: {Make}{Model} ({Year})\n Type: Electric Car\n Last Maintenance Date: {LastMaintenanceDate}\n Next Maintenance Date: {NextMaintenanceDate}");
        }

        public void Charge(DateTime timeOfCharge)
        {
            ChargeTime = timeOfCharge;
            ShowRefillTime();
        }

        public DateTime ShowLastChargeTime()
        {
            return ChargeTime;
        }

        public void ShowRefillTime()
        {
            Console.WriteLine($"ElectricCar {Make} {Model} refueled on {ChargeTime}");
        }
    }

}