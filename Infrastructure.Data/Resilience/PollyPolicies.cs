using System;
using System.Net.Http;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;

namespace Infrastructure.Data.Resilience
{
    public static class PollyPolicies
    {
        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError() 
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.TooManyRequests) 
                .WaitAndRetryAsync(
                    retryCount: 3,
                    sleepDurationProvider: retryAttempt =>
                        TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), 
                    onRetry: (outcome, delay, retryAttempt, ctx) =>
                    {
                        Console.WriteLine($"[AUTH0] Retry {retryAttempt} in {delay.TotalSeconds} seconds. Status: {outcome.Result?.StatusCode}");
                    });
        }

        public static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(
                    handledEventsAllowedBeforeBreaking: 5,
                    durationOfBreak: TimeSpan.FromSeconds(30),
                    onBreak: (outcome, timespan) =>
                    {
                        Console.WriteLine($"[AUTH0] Circuit OPEN for {timespan.TotalSeconds} seconds.");
                    },
                    onReset: () =>
                    {
                        Console.WriteLine("[AUTH0] Circuit CLOSED.");
                    },
                    onHalfOpen: () =>
                    {
                        Console.WriteLine("[AUTH0] Circuit HALF-OPEN. Testing...");
                    });
        }

        public static IAsyncPolicy<HttpResponseMessage> GetTimeoutPolicy()
        {
            return Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(3), TimeoutStrategy.Optimistic);
        }
    }
}
