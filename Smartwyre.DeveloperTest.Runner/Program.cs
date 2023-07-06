using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using System;

namespace Smartwyre.DeveloperTest.Runner;

class Program
{
    static void Main(string[] args)
    {
        using IHost host = CreateHostBuilder(args).Build();
        using var scope = host.Services.CreateScope();

        var services = scope.ServiceProvider;

        try
        {
            services.GetRequiredService<ServiceHelper>().Run(args);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        IHostBuilder CreateHostBuilder(string[] strings)
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices((_, services) =>
                {
                    services.AddScoped<IProductDataStore, ProductDataStore>();
                    services.AddScoped<IRebateDataStore, RebateDataStore>();
                    services.AddSingleton<IRebateService, RebateService>();
                    services.AddSingleton<ServiceHelper>();
                });
        }
    }
}
