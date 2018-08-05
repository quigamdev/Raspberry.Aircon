using System;
using System.Collections.Generic;
using System.Text;

namespace Raspberry.Aircon.Models
{
    public class TemperatureModel : IValidatableDataContract, IDataContract
    {
        public int Temperature { get; set; }


        public IEnumerable<ValidationResult> Validate()
        {
            if (Temperature > 30)
                yield return new ValidationResult("Temperature maximum is 30!");
            if (Temperature < 17)
                yield return new ValidationResult("Temperature minimum is 17!");
        }
    }
}
