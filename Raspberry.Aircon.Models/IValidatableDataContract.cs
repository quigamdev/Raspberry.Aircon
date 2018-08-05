using System.Collections.Generic;

namespace Raspberry.Aircon.Models
{
    public interface IValidatableDataContract
    {
        IEnumerable<ValidationResult> Validate();
    }
}