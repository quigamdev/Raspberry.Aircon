using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raspberry.SignalR.Operations;

namespace Raspberry.Aircon.Models.Validations
{
    public class NoOperationDataValidator : IRpiOperationDataValidator
    {
        private readonly RpiOperationContracts[] supported;

        public NoOperationDataValidator()
        {
            supported = new[]
            {
                RpiOperationContracts.StopAirConditioner,
                RpiOperationContracts.SwitchOffLedLights,
                RpiOperationContracts.SwitchOnLedLights
            };
        }

        public IEnumerable<ValidationResult> Validate(IDataContract data)
        {
            return new List<ValidationResult>();
        }

        public bool IsModelSupported(params RpiOperationContracts[] contracts)
        {
         
            return contracts.Any(s => supported.Contains(s));
        }
    }
}
