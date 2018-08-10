using System;
using System.Collections.Generic;
using System.Text;
using Raspberry.Aircon.Models;
using Raspberry.Aircon.PiController.Facades;
using Raspberry.SignalR.Operations;

namespace Raspberry.Aircon.PiController.Operations
{
    public class SwitchOnLedLightsOperation : RpiOperation
    {
        public override void Execute(IOperationContract<RpiOperationContracts> contract)
        {
            new LedLightsFacade().SwitchOn();
        }

        public override RpiOperationContracts OperationIdentifier { get; } = RpiOperationContracts.SwitchOnLedLights;
    }
}
