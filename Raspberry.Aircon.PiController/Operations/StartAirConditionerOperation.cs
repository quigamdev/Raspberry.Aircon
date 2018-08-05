using System;
using System.Collections.Generic;
using System.Text;
using Raspberry.Aircon.Models;

namespace Raspberry.Aircon.PiController.Operations
{
    public class StartAirConditionerOperation : OperationBase
    {
        public override OperationContracts OperationIdentifier { get; } = OperationContracts.StartAirConditioner;
        public override void Execute(OperationContract contract)
        {
            var data = (TemperatureModel)contract.Data;
            Program.Log($"Operation by OpContract {contract.Id}");
            Program.Log($"Conditioner started on {data.Temperature}");
        }

        protected override bool ValidateType(OperationContract contract)
        {
            return contract.Data is TemperatureModel;
        }
    }
}
