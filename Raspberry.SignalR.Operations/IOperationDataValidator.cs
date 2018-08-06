using System.Collections.Generic;

namespace Raspberry.SignalR.Operations
{
    public interface IOperationDataValidator<T>
    {
        IEnumerable<ValidationResult> Validate(IDataContract data);
        bool IsModelSupported(params T[] contracts);
    }
}