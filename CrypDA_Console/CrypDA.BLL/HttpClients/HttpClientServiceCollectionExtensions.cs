using System;
using CrypDA.BLL.HttpClients.BinanceClients;
using CrypDA.BLL.HttpClients.RetryPolicies;
using CrypDA.Infrastructure.HttpClients.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CrypDA.BLL.HttpClients
{
    public static class HttpClientServiceCollectionExtensions
    {
        public static IServiceCollection AddBinanceClient(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.GetSection(BinanceClientSettings.SectionName)
                .Get<BinanceClientSettings>();

            var retryPolicySettings = configuration.GetSection(DefaultRetryPolicySettings.SectionName)
                .Get<DefaultRetryPolicySettings>();

            services.AddHttpClient<IBinanceClient, BinanceClient>(client =>
                {
                    client.BaseAddress = new Uri(settings.Host);
                })
                .AddPolicyHandler(RetryPolicyExtensions.GetRetryPolicy(retryPolicySettings, settings));

            return services;
        }
    }
}
