using System.Collections.Generic;

namespace Raspberry.SignalR.Operations
{
    public abstract class OperationBase<T> : IOperation<T>
    {
        public abstract void Execute(IOperationContract<T> contract);

        public IEnumerable<ValidationResult> Validate(IOperationContract<T> contract)
        {
            var validator = ValidatorResolver<T>.Resolve(contract);
            return validator.Validate(contract.Data);
        }

        public abstract T OperationIdentifier { get; }
    }
}