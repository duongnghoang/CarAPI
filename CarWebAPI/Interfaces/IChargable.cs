namespace CarWebAPI.Interfaces
{
    public interface IChargable
    {
        void Charge(DateTime timeOfCharge);
        DateTime ShowLastChargeTime();
    }
}