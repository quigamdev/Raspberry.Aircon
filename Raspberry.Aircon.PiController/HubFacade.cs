using System;
using System.Linq;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
            var url = configuration.GetValue<string>("HubUrl");
            builder.WithUrl(url);
            logger.LogTrace($"Creating hub for {url}");
            if (bool.Parse(configuration["HubDebugLogging"]))
            {
                builder.ConfigureLogging(b =>
                {
                    b.AddProvider(new RpiConsoleLoggerProvider());
                });
            }

            Client = builder.Build();
            Client.Closed += TryReconnect;
            Client.On<RpiOperationContract>("ServerToClient", ReceiveMessage);
        }

        private async Task TryReconnect(Exception arg)
        {
            if (RequestedExit) return;
            logger.LogTrace("Connection lost ...");
            var connected = false;
            while (!connected)
            {
                await Task.Delay(2000);
                logger.LogTrace("Trying to reconnect ...");
                try
                {
                    StartListen();
                    connected = true;
                }
                catch (Exception e)
                {
                }
            }
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
            this.RequestedExit = true;
            logger.LogInformation("Disconnecting from server");
            Task.Run(async () => { await Client.StopAsync(); }).Wait();
        }

        private bool RequestedExit { get; set; }

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
                    logger.LogError(result.Message ?? "Generic error occurred");
                }
            }
            else
            {
                logger.LogInformation($"Executing operation :{contract.Operation}");
                operation.Execute(contract);
            }
        }
    }
}