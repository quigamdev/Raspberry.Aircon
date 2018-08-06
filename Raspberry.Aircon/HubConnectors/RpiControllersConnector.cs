using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Raspberry.Aircon.Interface.Hubs;
using Raspberry.Aircon.Models;
using Raspberry.SignalR.Operations;

namespace Raspberry.Aircon.Interface.HubConnectors
{
    public class RpiControllersConnector : IHubConnector
    {
        private IHubContext<RpiControllersHub> Hub { get; set; }
        public RpiControllersConnector(IHubContext<RpiControllersHub> hub)
        {
            Hub = hub;
        }

        public async Task SendContract(IRpiOperationContract contract)
        {
            //validate operation data
            var validator = ValidatorResolver<RpiOperationContracts>.Resolve(contract);
            var results = validator.Validate(contract.Data).ToList();
            if (results.Any())
            {
                throw new OperationDataValidationException(results);
            }
            
            // send to clients
            await Hub.Clients.All.SendCoreAsync("ServerToClient", new object[] { contract });
        }
    }
}
