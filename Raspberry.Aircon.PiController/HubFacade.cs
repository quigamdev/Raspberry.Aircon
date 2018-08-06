using System;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Newtonsoft.Json;
using Raspberry.Aircon.Models;
using Raspberry.SignalR.Operations;

namespace Raspberry.Aircon.PiController
{
    public class HubFacade
    {
        private readonly ILogger logger;

        public HubFacade(IConfigurationRoot configuration, ILogger logger)
        {
            this.logger = logger;
            var builder = new HubConnectionBuilder();
            builder.AddJsonProtocol(options =>
            {
                //set serialization options to be able to deserialize interfaces
                options.PayloadSerializerSettings.TypeNameHandling = TypeNameHandling.Objects;
            });
            builder.WithUrl(configuration.GetValue<string>("HubUrl"));
            if (bool.Parse(configuration["HubDebugLogging"]))
            {
                builder.ConfigureLogging(b =>
                {
                    b.AddProvider(new RpiConsoleLoggerProvider());
                });
            }

            Client = builder.Build();
            Client.On<RpiOperationContract>("ServerToClient", ReceiveMessage);
        }

        public void SendContract(IRpiOperationContract contract)
        {
            Client.SendAsync("ClientToServer", contract);
        }

        public void StartListen()
        {
            logger.LogInformation("Connecting to server");
            try
            {
                Task.Run(async () => { await Client.StartAsync(); }).Wait();
            }
            catch (Exception e)
            {
                logger.LogCritical("Cannot connect to server.");
                throw;
            }
            logger.LogInformation("Connected to server");
        }

        public void StopListen()
        {
            logger.LogInformation("Disconnecting from server");
            Task.Run(async () => { await Client.StopAsync(); }).Wait();
        }

        public HubConnection Client { get; set; }

        private void ReceiveMessage(RpiOperationContract contract)
        {
            logger.LogInformation($"Received contract : {contract}");
            var operation = Program.OperationResolver.Resolve(contract);
            var validation = operation.Validate(contract).ToList();
            if (validation.Any())
            {
                foreach (var result in validation)
                {
                    logger.LogError(result.Message);
                }
            }
            else
            {
                operation.Execute(contract);
            }
        }
    }
}