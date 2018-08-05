using System;
using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Newtonsoft.Json;
using Raspberry.Aircon.Models;

namespace Raspberry.Aircon.PiController
{
    class Program
    {
        private static OperationResolver Resolver { get; } = new OperationResolver();
        static void Main(string[] args)
        {
            Log("--------------------------------------");
            Log("Air Conditioner Controller Client");
            Log("--------------------------------------");

            IConfigurationRoot configuration = GetConfiguration();

            Resolver.Initialize();
            Log("Operations ready");

            HubConnection client = BuildHubConnection(configuration);
            client.On<OperationContract>("ReceiveMessage", ReceiveMessage);
            client.StartAsync();

            Log("Connecting to server");


            // Wait for messages - service mode
            while (true)
            {
                Thread.Sleep(100000);
            }
        }

        private static HubConnection BuildHubConnection(IConfigurationRoot configuration)
        {
            var builder = new HubConnectionBuilder();
            builder.AddJsonProtocol(options =>
            {
                //set serialization options to be able to deserialize interfaces
                options.PayloadSerializerSettings.TypeNameHandling = TypeNameHandling.Objects;
            });
            builder.WithUrl(configuration.GetValue<string>("HubUrl"));
            builder.ConfigureLogging(b =>
            {
                b.AddProvider(new ConsoleLoggerProvider((s, level) =>
                {
                    Log(s);
                    return true;
                }, true));
            });
            var client = builder.Build();
            return client;
        }

        private static IConfigurationRoot GetConfiguration()
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true);

            IConfigurationRoot configuration = configBuilder.Build();
            return configuration;
        }

        private static void ReceiveMessage(OperationContract contract)
        {
            Log("Message received");
            Log($"Contract name: {contract.ToString()}");
            var operation = Resolver.Resolve(contract);
            if (!operation.Validate(contract))
            {
                Log("INVALID DATA", true);
            }
            else
            {
                operation.Execute(contract);
            }
        }

        public static void Log(string message, bool isError = false)
        {
            Console.ForegroundColor = isError ? ConsoleColor.Red : ConsoleColor.Green;
            Console.WriteLine($"(#{Thread.CurrentThread.ManagedThreadId}) {DateTime.Now}: {message}");
            Console.ResetColor();
        }
    }
}
