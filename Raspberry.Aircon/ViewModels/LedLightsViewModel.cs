using System.Threading.Tasks;
using DotVVM.Framework.Hosting;
using Raspberry.Aircon.Interface.HubConnectors;
using Raspberry.Aircon.Models;

namespace Raspberry.Aircon.Interface.ViewModels
{
    public class LedLightsViewModel : HubViewModel
    {
        private IDotvvmRequestContext context;

        public LedLightsViewModel(IDotvvmRequestContext context) : base(context)
        {
            this.context = context;
        }
        public async Task SwitchOn()
        {
            var contract = new RpiOperationContract()
            {
                Operation = RpiOperationContracts.SwitchOnLedLights
            };

            RpiControllersConnector hub = GetHub();
            await hub.SendContract(contract);
        }
        public async Task SwitchOff()
        {
            var contract = new RpiOperationContract()
            {
                Operation = RpiOperationContracts.SwitchOffLedLights
            };

            RpiControllersConnector hub = GetHub();
            await hub.SendContract(contract);
        }
    }
}