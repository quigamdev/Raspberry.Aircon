using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Raspberry.Aircon.Models;
using Raspberry.SignalR.Operations;

namespace Raspberry.Aircon.PiController.Operations
{
    public class DefaultOperation : RpiOperation
    {
        public override void Execute(IOperationContract<RpiOperationContracts> contract)
        {
            Logger.LogError("Invalid operation.");
        }

        public override RpiOperationContracts OperationIdentifier { get; } = RpiOperationContracts.UnknownContract;
    }
}
