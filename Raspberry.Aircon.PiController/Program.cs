using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Raspberry.Aircon.Models;
using Raspberry.Aircon.PiController.Operations;
using Raspberry.SignalR.Operations;

namespace Raspberry.Aircon.PiController
{
    class Program
    {
        public static OperationResolver<RpiOperationContracts> OperationResolver { get; } = new OperationResolver<RpiOperationContracts>();
        private static HubFacade Hub { get; set; }
        public static ILogger Logger { get; } = new ConsoleLogger("Default", (s, level) => true, true);
        private static int ExitAppRequests { get; set; }

        static void Main(string[] args)
        {
            RegisterExitHandler();

            Logger.LogInformation("Air Conditioner Controller Client");

            IConfigurationRoot configuration = GetConfiguration();

            OperationResolver.Initialize(typeof(Program).Assembly, new DefaultOperation());

            Logger.LogInformation("Operations ready");

            Hub= new HubFacade(configuration, Logger);
            Hub.StartListen();

            // Wait for messages - service mode
            while (true)
            {
                Console.ReadKey();
            }
            // ReSharper disable once FunctionNeverReturns
        }

        private static void RegisterExitHandler()
        {
            Console.CancelKeyPress += new ConsoleCancelEventHandler((sender, eventArgs) =>
            {
                ExitAppRequests++;
                Logger.LogInformation("Exiting application");
                Hub.StopListen();
                Environment.Exit(0);
            });
        }

        private static IConfigurationRoot GetConfiguration()
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true);

            IConfigurationRoot configuration = configBuilder.Build();
            return configuration;
        }
    }
}
