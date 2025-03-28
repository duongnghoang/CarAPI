namespace NashTechAssignment
{
    public interface IFuelable
    {
        void Refuel(DateTime timeOfRefuel);
        DateTime ShowLastFuelTime();
    }
}