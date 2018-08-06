using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Raspberry.Aircon.Interface.HubConnectors;
using Raspberry.Aircon.Interface.Hubs;
using Raspberry.Aircon.Models;

namespace Raspberry.Aircon.Interface.ViewModels
{
    public class DefaultViewModel : MasterPageViewModel
    {
        public string Title { get; set; }
        public AirConditionerViewModel AirConditionerViewModel { get; set; }

        public DefaultViewModel()
        {
        }

        public override Task Init()
        {
            AirConditionerViewModel = new AirConditionerViewModel(Context);
            return base.Init();
        }
    }
}
