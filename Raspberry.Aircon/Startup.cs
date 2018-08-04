using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using DotVVM.Framework.Hosting;
using Newtonsoft.Json.Serialization;
using Raspberry.Aircon.Hubs;


namespace Raspberry.Aircon
{
    public class Startup
    {

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDataProtection();
            services.AddAuthorization();
            services.AddWebEncoders();
            services.AddSignalR().AddHubOptions<RpiControllersHub>(options =>
                {
                    options.EnableDetailedErrors = true;
                    options.HandshakeTimeout = TimeSpan.FromSeconds(30);
                })
                .AddJsonProtocol(options =>
            {
                options.PayloadSerializerSettings.ContractResolver = new DefaultContractResolver();
            });
            services.AddDotVVM();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            Console.WriteLine($"{nameof(env.WebRootPath)} : '{env.WebRootPath}'");
            Console.WriteLine($"{nameof(env.ContentRootFileProvider)} : '{env.ContentRootFileProvider != null}'");
            Console.WriteLine($"{nameof(env.ContentRootPath)} : '{env.ContentRootPath}'");
            Console.WriteLine($"{nameof(env.EnvironmentName)} : '{env.EnvironmentName}'");

            app.UseSignalR(builder =>
            {
                builder.MapHub<RpiControllersHub>("/hub");
            });

            // use DotVVM
            var dotvvmConfiguration = app.UseDotVVM<DotvvmStartup>(env.ContentRootPath);

            // use static files
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(env.WebRootPath ?? Path.Combine(env.ContentRootPath, "wwwroot"))
            });
        }
    }
}
