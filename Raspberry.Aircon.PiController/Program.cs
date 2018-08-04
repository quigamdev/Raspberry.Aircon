using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace Raspberry.Aircon.PiController
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var builder = new Microsoft.AspNetCore.SignalR.Client.HubConnectionBuilder();
            builder.WithUrl("http://localhost:9249/hub");
            var client  =builder.Build();
            client.On<string, string>("ReceiveMessage", ReceiveMessage );
            await client.StartAsync();
            while (true)
            {
                await Task.Delay(100);
            }
        }

        private static void ReceiveMessage(string arg1, string arg2)
        {

            Console.WriteLine("arg1:");
            Console.WriteLine(arg1);
            Console.WriteLine("arg2:");
            Console.WriteLine(arg2);

        }
    }
}
