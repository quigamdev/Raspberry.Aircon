using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Raspberry.Aircon.Hubs
{
    public class RpiControllersHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.Client(Context.ConnectionId)
                .SendCoreAsync("ReceiveMessage", new[] {"art1 completed", "arg2 completed"});

        }
    }
}