using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Raspberry.Aircon.Models;

namespace Raspberry.Aircon.PiController.Operations
{
    public interface IOperation
    {
        void Execute(OperationContract contract);
        bool Validate(OperationContract contract);
        OperationContracts OperationIdentifier { get; }
    }
    public abstract class OperationBase : IOperation
    {
        public abstract void Execute(OperationContract contract);

        public bool Validate(OperationContract contract)
        {
            if (!ValidateType(contract))
                return false;
            var data = contract.Data as IValidatableDataContract;
            if (data != null)
            {
                return !data.Validate().Any();
            }
            return true;
        }

        protected abstract bool ValidateType(OperationContract contract);
        public abstract OperationContracts OperationIdentifier { get; }
    }
}
