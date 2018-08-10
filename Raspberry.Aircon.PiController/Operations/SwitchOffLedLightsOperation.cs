using Raspberry.Aircon.Models;
using Raspberry.Aircon.PiController.Facades;
using Raspberry.SignalR.Operations;

namespace Raspberry.Aircon.PiController.Operations
{
    public class SwitchOffLedLightsOperation : RpiOperation
    {
        public override void Execute(IOperationContract<RpiOperationContracts> contract)
        {
            new LedLightsFacade().SwitchOff();
        }

        public override RpiOperationContracts OperationIdentifier { get; } = RpiOperationContracts.SwitchOffLedLights;
    }
}