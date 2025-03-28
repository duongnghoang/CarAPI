namespace NashTechAssignment
{
    public abstract class Car
    {
        public string make;
        public string model;
        public int year;
        public DateTime lastMaintenanceDate;
        public DateTime nextMaintenanceDate;

        public Car(string make, string model, int year, DateTime lastMaintenanceDate)
        {
            this.make = make;
            this.model = model;
            this.year = year;
            this.lastMaintenanceDate = lastMaintenanceDate;
        }

        public abstract void DisplayDetails();

        // Method to schedule the next maintenance (6 months later)
        public void ScheduleMaintenance()
        {
            nextMaintenanceDate = lastMaintenanceDate.AddMonths(6);
        }
    }
}