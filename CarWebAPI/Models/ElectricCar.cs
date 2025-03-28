using CarWebAPI.Interfaces;

namespace CarWebAPI.Models
{
    public class ElectricCar : Car, IChargable, IShowRefill
    {
        public DateTime ChargeTime;
        public ElectricCar(int id, string make, string model, int year, DateTime lastMaintenanceDate)
            : base(id, make, model, year, lastMaintenanceDate)
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