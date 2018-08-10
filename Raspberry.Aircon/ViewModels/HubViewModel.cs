using DotVVM.Framework.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Raspberry.Aircon.Interface.HubConnectors;

namespace Raspberry.Aircon.Interface.ViewModels
{
    public class HubViewModel
    {
        public HubViewModel(IDotvvmRequestContext context)
        {
            Context = context;
        }

        protected IDotvvmRequestContext Context { get;  }

        protected RpiControllersConnector GetHub()
        {
            return Context.Services.GetRequiredService<RpiControllersConnector>();
        }
    }
}