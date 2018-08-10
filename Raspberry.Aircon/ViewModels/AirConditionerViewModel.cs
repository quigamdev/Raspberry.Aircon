using System;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.ViewModel.Validation;
using Microsoft.AspNetCore.Hosting.Internal;
using Raspberry.Aircon.Interface.HubConnectors;
using Raspberry.Aircon.Interface.Hubs;
using Raspberry.Aircon.Models;
using Raspberry.Aircon.Models.Models;
using Raspberry.SignalR.Operations;

namespace Raspberry.Aircon.Interface.ViewModels
{
    public class AirConditionerViewModel : HubViewModel
    {
        private IDotvvmRequestContext Context { get; }

        public AirConditionerViewModel(IDotvvmRequestContext context) : base(context)
        {
            Context = context;
        }

        public int Temperature { get; set; } = 24;

        public async Task Stop()
        {
            var contract = new RpiOperationContract()
            {
                Operation = RpiOperationContracts.StopAirConditioner
            };

            RpiControllersConnector hub = GetHub();
            await hub.SendContract(contract);
        }

        public async Task Start()
        {
            var contract = new RpiOperationContract()
            {
                Data = new TemperatureModel() { Temperature = Temperature },
                Operation = RpiOperationContracts.StartAirConditioner
            };
            RpiControllersConnector hub = GetHub();

            try
            {
                await hub.SendContract(contract);
            }
            catch (OperationDataValidationException e)
            {
                Context.ModelState.Errors.AddRange(e.Results.Select(s => new ViewModelValidationError() { ErrorMessage = s.Message }));
                Context.FailOnInvalidModelState();
            }
        }


    }
}