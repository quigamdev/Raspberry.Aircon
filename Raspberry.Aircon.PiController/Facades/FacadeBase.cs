using Microsoft.Extensions.Logging;

namespace Raspberry.Aircon.PiController.Facades
{
    public class FacadeBase
    {
        public ILogger Logger => Program.Logger;
    }
}