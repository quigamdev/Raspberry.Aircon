using System.Collections.Generic;

namespace Raspberry.SignalR.Operations
{
    public interface IOperation<T>
    {
        void Execute(IOperationContract<T> contract);
        IEnumerable<ValidationResult> Validate(IOperationContract<T> contract);
        T OperationIdentifier { get; }
    }
}
