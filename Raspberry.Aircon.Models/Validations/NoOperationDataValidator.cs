using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raspberry.SignalR.Operations;

namespace Raspberry.Aircon.Models.Validations
{
    public class NoOperationDataValidator : IRpiOperationDataValidator
    {
        public IEnumerable<ValidationResult> Validate(IDataContract data)
        {
            return new List<ValidationResult>();
        }

        public bool IsModelSupported(params RpiOperationContracts[] contracts)
        {
            var supported = new[] { RpiOperationContracts.StopAirConditioner };
            return contracts.Any(s => supported.Contains(s));
        }
    }
}
