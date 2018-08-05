using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.SignalR;
using Raspberry.Aircon.Models;
namespace Raspberry.Aircon.Interface.Hubs
{
    public class RpiControllersHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            
            await Clients.Client(Context.ConnectionId)
                .SendCoreAsync("ReceiveMessage", new[] { new OperationContract()
                {
                    Data = new TemperatureModel(){ Temperature = 18 }, 
                    Operation = OperationContracts.StartAirConditioner
                }});

        }
    }
}