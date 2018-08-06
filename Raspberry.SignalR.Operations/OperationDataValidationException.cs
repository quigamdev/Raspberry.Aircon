using System;
using System.Collections.Generic;

namespace Raspberry.SignalR.Operations
{
    public class OperationDataValidationException : Exception
    {
        public IEnumerable<ValidationResult> Results { get; }

        public OperationDataValidationException(IEnumerable<ValidationResult> results)
        {
            Results = results;
        }
    }
}