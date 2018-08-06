using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Raspberry.SignalR.Operations
{
    public class OperationResolver<T>
    {
        public void Initialize(Assembly assemblyContainingOperations, IOperation<T> defaultOperation)
        {
            DefaultOperation = defaultOperation;
            var operationBase = typeof(IOperation<T>);
            AllOperations = GetAssemblyTypes(assemblyContainingOperations).Where(s => operationBase.IsAssignableFrom(s) && s.IsClass && !s.IsAbstract)
                .Select(s => (IOperation<T>)Activator.CreateInstance(s, true)).ToList();
            AssertConfigurationIsValid();
        }

        private IEnumerable<IOperation<T>> AllOperations { get; set; }

        public IOperation<T> Resolve(IOperationContract<T> contract)
        {
            var operation = AllOperations.FirstOrDefault(s => s.OperationIdentifier.Equals(contract.Operation));
            if (operation != null)
            {
                return operation;
            }

            return DefaultOperation ?? throw new OperationsResolverException($"Cannot find operation for '{contract.Operation.ToString()}' and default operation is not set!");
        }

        public IOperation<T> DefaultOperation { get; private set; }

        public void SetDefaultOperation(IOperation<T> operation)
        {
            DefaultOperation = operation;
        }

        private List<Type> GetAssemblyTypes(Assembly assemblyContainingOperations)
        {
            try
            {
                return assemblyContainingOperations.GetTypes().ToList();
            }
            catch (ReflectionTypeLoadException e)
            {
                return e.Types.ToList();
            }
        }

        public void AssertConfigurationIsValid()
        {
            var sb = new StringBuilder();
            var identifiers = AllOperations.GroupBy(s => s.OperationIdentifier);
            var duplicated = identifiers.Where(s => s.Count() > 1).ToList();

            if (!duplicated.Any())
            {
                // no duplicity - OK
                return;
            }

            var errors = new List<Exception>();
            foreach (var duplicityGroup in duplicated)
            {
                sb.Clear();
                sb.AppendLine(
                    $"Operation identifier {duplicityGroup.First().OperationIdentifier} is duplicated in types: ");
                foreach (var s1 in duplicityGroup.Select(s => s.GetType().FullName))
                {
                    sb.AppendLine(s1);
                }
                errors.Add(new OperationsResolverException(sb.ToString()));
            }
        }
    }
}
