using System;
using System.Collections.Generic;
using System.Text;
using Raspberry.Aircon.Models;
using Raspberry.Aircon.PiController.Facades;
using Raspberry.SignalR.Operations;

namespace Raspberry.Aircon.PiController.Operations
{
    class StopAirConditionerOperation: RpiOperation
    {
        public override void Execute(IOperationContract<RpiOperationContracts> contract)
        {
            new AirConditionerFacade().Stop();
        }

        public override RpiOperationContracts OperationIdentifier { get; } = RpiOperationContracts.StopAirConditioner;
    }
}
