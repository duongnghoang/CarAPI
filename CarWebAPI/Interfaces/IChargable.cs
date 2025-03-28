namespace NashTechAssignment
{
    public interface IChargable
    {
        void Charge(DateTime timeOfCharge);
        DateTime ShowLastChargeTime();
    }
}