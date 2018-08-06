using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Raspberry.SignalR.Operations
{
    public static class ValidatorResolver<T>
    {
        private static BlockingCollection<IOperationDataValidator<T>> Validators = null;

        private static object locker = new object();

        public static void Initialize(Assembly assemblyContainingValidators = null)
        {
            if (Validators == null)
            {
                lock (locker)
                {
                    if (Validators == null)
                    {
                        var operationBase = typeof(IOperationDataValidator<T>);
                        Validators = new BlockingCollection<IOperationDataValidator<T>>();
                        foreach (var validator in GetAssemblyTypes(assemblyContainingValidators).Where(s => operationBase.IsAssignableFrom(s) && s.IsClass && !s.IsAbstract)
                            .Select(s => (IOperationDataValidator<T>)Activator.CreateInstance(s, true)))
                        {
                            Validators.Add(validator);
                        }
                    }
                }
            }
        }

        private static List<Type> GetAssemblyTypes(Assembly assemblyContainingValidators = null)
        {
            try
            {
                return (assemblyContainingValidators ?? typeof(T).Assembly).GetTypes().ToList();
            }
            catch (ReflectionTypeLoadException e)
            {
                return e.Types.ToList();
            }
        }

        public static IOperationDataValidator<T> Resolve(IOperationContract<T> contract)
        {
            if (Validators == null)
            {
                Initialize();
            }

            // ReSharper disable once AssignNullToNotNullAttribute
            var validators = Validators.Where(s => s.IsModelSupported(contract.Operation)).ToList();
            if (validators.Count == 1)
            {
                return validators[0];
            }

            if (validators.Count > 1)
                throw new OperationDataValidatorResolverException(
                    $"There is more then one validator for specified operation '{contract.Operation.ToString()}'");
            throw new OperationDataValidatorResolverException($"Cannot find validator for specified operation '{contract.Operation.ToString()}'");

        }
    }
}
