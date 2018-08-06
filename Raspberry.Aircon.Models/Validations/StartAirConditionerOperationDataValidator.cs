using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Raspberry.Aircon.Models.Models;
using Raspberry.SignalR.Operations;

namespace Raspberry.Aircon.Models.Validations
{
    public class StartAirConditionerOperationDataValidator : IRpiOperationDataValidator
    {
        public IEnumerable<ValidationResult> Validate(IDataContract data)
        {
            var temperature = (data as TemperatureModel)?.Temperature;
            if (temperature == null)
                yield return new ValidationResult("Model is not valid for this operation!");

            if (temperature > 30)
                yield return new ValidationResult("Temperature maximum is 30!");
            if (temperature < 17)
                yield return new ValidationResult("Temperature minimum is 17!");
        }

        public bool IsModelSupported(params RpiOperationContracts[] contracts)
        {
            return contracts.Any(s => s == RpiOperationContracts.StartAirConditioner);
        }
    }

}
