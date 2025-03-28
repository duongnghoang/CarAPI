namespace CarWebAPI.Interfaces
{
    public interface IFuelable
    {
        void Refuel(DateTime timeOfRefuel);
        DateTime ShowLastFuelTime();
    }
}