using System;

namespace Raspberry.SignalR.Operations
{
    public class OperationContract<T> : IOperationContract<T>
    {
        /// <summary>
        /// Unique identifier of contract
        /// DO NOT CHANGE !! NEVER !!
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();
        public T Operation { get; set; }    
        public IDataContract Data { get; set; }

        public override string ToString()
        {
            return Operation.ToString();
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            var contract = obj as IOperationContract<T>;
            if (contract == null) return false;
            return contract.Id == Id;
        }

        public override int GetHashCode()
        {
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return Id.GetHashCode();
        }
    }
}
