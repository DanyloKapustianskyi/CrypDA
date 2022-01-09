using System;
using CrypDA.BLL.HttpClients;
using CrypDA.BLL.HttpClients.BinanceClients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CrypDA
{
    class Program
    {
        public static IConfiguration Configuration { get; set; }

        static void Main(string[] args)
        {
            //var configurationBuilder = new ConfigurationBuilder();
            //Configuration = configurationBuilder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();

            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    var serviceProvider = services.BuildServiceProvider();
                    Configuration = serviceProvider.GetService<IConfiguration>();
                    services.AddBinanceClient(Configuration);

                }).ConfigureAppConfiguration((hostContext, configurationBuilder) =>
                {
                    configurationBuilder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                }).UseConsoleLifetime().Build();


        }
    }
}
