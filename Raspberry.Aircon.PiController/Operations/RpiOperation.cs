using Microsoft.Extensions.Logging;
using Raspberry.Aircon.Models;
using Raspberry.SignalR.Operations;

namespace Raspberry.Aircon.PiController.Operations
{
    public abstract class RpiOperation: OperationBase<RpiOperationContracts>
    {
        public ILogger Logger => Program.Logger;

    }
}