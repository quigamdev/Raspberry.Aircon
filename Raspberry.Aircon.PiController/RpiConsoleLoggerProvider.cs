using Microsoft.Extensions.Logging;

namespace Raspberry.Aircon.PiController
{
    public class RpiConsoleLoggerProvider : ILoggerProvider
    {
        public void Dispose()
        {
        }

        public ILogger CreateLogger(string categoryName)
        {
            return Program.Logger;
        }
    }
}