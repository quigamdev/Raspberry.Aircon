﻿using System;
using System.Collections.Generic;
using System.Text;
using Raspberry.Aircon.Models;
using Raspberry.Aircon.Models.Models;
using Raspberry.Aircon.PiController.Facades;
using Raspberry.SignalR.Operations;

namespace Raspberry.Aircon.PiController.Operations
{
    public class StartAirConditionerOperation : RpiOperation
    {
        public override RpiOperationContracts OperationIdentifier { get; } = RpiOperationContracts.StartAirConditioner;
        public override void Execute(IOperationContract<RpiOperationContracts> contract)
        {
            var data = (TemperatureModel)contract.Data;
            new AirConditionerFacade().Start(data.Temperature);
        }
    }
}
