using Microsoft.Extensions.Logging;

namespace Raspberry.Aircon.PiController.Facades
{
    public class AirConditionerFacade : FacadeBase
    {
        public void Start(int temperature)
        {
            Logger.LogInformation($"Starting air conditioner {temperature}.");
        }

        public void Stop()
        {
            Logger.LogInformation($"Stopping air conditioner.");
        }
    }
}
