using Raspberry.SignalR.Operations;

namespace Raspberry.Aircon.Models.Models
{
    public class TemperatureModel : IDataContract
    {
        public int Temperature { get; set; }
    }
}
