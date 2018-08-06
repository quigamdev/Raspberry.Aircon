using System;

namespace Raspberry.SignalR.Operations
{
    public interface IOperationContract<T>
    {
        IDataContract Data { get; set; }
        Guid Id { get; set; }
        T Operation { get; set; }

        bool Equals(object obj);
        int GetHashCode();
        string ToString();
    }
}