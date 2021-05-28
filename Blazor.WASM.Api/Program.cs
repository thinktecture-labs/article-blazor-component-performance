using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazor.WASM.Api.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Blazor.WASM.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();

            // Create a new scope
            using (var scope = host.Services.CreateScope())
            {
                // Get the DbContext instance
                var peopleService = scope.ServiceProvider.GetRequiredService<PeopleService>();

                //Do the migration asynchronously
                await peopleService.InitAsync();
            }

            // Run the WebHost, and start accepting requests
            // There's an async overload, so we may as well use it
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}