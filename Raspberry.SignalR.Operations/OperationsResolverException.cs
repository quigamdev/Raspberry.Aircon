using System;

namespace Raspberry.SignalR.Operations
{
    public class OperationsResolverException : Exception
    {
        public OperationsResolverException(string message) : base(message)
        {
        }
    }
}