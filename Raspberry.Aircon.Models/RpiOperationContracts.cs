namespace Raspberry.Aircon.Models
{
    public enum RpiOperationContracts
    {
        UnknownContract = 0,
        StartAirConditioner = 1,
        StopAirConditioner = 2,
        SwitchOnLedLights = 3,
        SwitchOffLedLights = 4,
    }
}