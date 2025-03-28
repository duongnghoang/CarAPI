namespace CarWebAPI.Models
{
    public abstract class Car
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public DateTime LastMaintenanceDate { get; set; }
        public DateTime NextMaintenanceDate { get; set; }

        public Car(int id, string make, string model, int year, DateTime lastMaintenanceDate)
        {
            Id = id;
            Make = make;
            Model = model;
            Year = year;
            LastMaintenanceDate = lastMaintenanceDate;
        }

        public abstract void DisplayDetails();

        // Method to schedule the next maintenance (6 months later)
        public void ScheduleMaintenance()
        {
            NextMaintenanceDate = LastMaintenanceDate.AddMonths(6);
        }
    }
}