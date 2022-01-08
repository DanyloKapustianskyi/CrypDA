using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using CrypDA.Infrastructure.HttpClients.Interfaces;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Extensions.Http;

namespace CrypDA.Infrastructure.HttpClients.Extensions
{
    public static class RetryPolicyExtensions
    {
        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(IRetryPolicySettings retryPolicy, IHttpClientSettings clientSettings)
        {
            var maxDelay = TimeSpan.FromMilliseconds(retryPolicy.MaxDelayMs);
            var delay = Backoff
                .DecorrelatedJitterBackoffV2(
                    medianFirstRetryDelay: TimeSpan.FromMilliseconds(retryPolicy.FirstDelayMs),
                    retryCount: retryPolicy.RetryAttempts)
                .Select(s => TimeSpan.FromTicks(Math.Min(s.Ticks, maxDelay.Ticks)));

            return HttpPolicyExtensions

                // Handle HttpRequestExceptions, 408 and 5xx status codes
                .HandleTransientHttpError()

                // Wait and retry with jittered back-off
                .WaitAndRetryAsync(delay, (exception, sleepDuration, attemptNumber, context) =>
                {
                    //Log.Information(
                    //    $"Transient error while accessing {clientSettings.Host}. Retrying in {sleepDuration}. {attemptNumber} / {retryPolicy.RetryAttempts}");
                });
        }
    }
}
