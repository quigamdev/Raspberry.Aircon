using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Raspberry.Aircon.Models;
using Raspberry.Aircon.PiController.Operations;

namespace Raspberry.Aircon.PiController
{
    public class OperationResolver
    {
        public void Initialize()
        {
            var operationBase = typeof(IOperation);
            AllOperations = GetAssemblyTypes().Where(s => operationBase.IsAssignableFrom(s) && s.IsClass && !s.IsAbstract)
                .Select(s => (IOperation)Activator.CreateInstance(s, true)).ToList();
        }

        private IEnumerable<IOperation> AllOperations { get; set; }

        public IOperation Resolve(OperationContract contract)
        {
            return AllOperations.FirstOrDefault(s => s.OperationIdentifier == contract.Operation);
        }

        private List<Type> GetAssemblyTypes()
        {
            try
            {
                return GetType().Assembly.GetTypes().ToList();
            }
            catch (ReflectionTypeLoadException e)
            {
                return e.Types.ToList();
            }
        }
    }
}
