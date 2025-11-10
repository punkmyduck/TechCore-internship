using Polly;
using Polly.CircuitBreaker;
using Polly.Extensions.Http;
using BookService.Application.Services;
using Domain.Services;

namespace BookService.Extensions
{
    public static class HttpClientExtensions
    {
        public static void AddJsonPlaceholderHttpClient(this IServiceCollection services)
        {
            var retryPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(1));

            var circuitBreakerPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));

            var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(3));

            var fallbackPolicy = Policy<HttpResponseMessage>
                .Handle<BrokenCircuitException>()
                .Or<TimeoutException>()
                .FallbackAsync(
                    fallbackValue: new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                    {
                        Content = new StringContent("{\"userId\": 1, \"id\": 1, \"title\": \"delectus aut autem\", \"completed\": false}")
                    },
                    onFallbackAsync: ex =>
                    {
                        Console.WriteLine("Circuit open or timeout — returning fallback response");
                        return Task.CompletedTask;
                    }
                );

            var combinedPolicy = Policy.WrapAsync(fallbackPolicy, circuitBreakerPolicy, retryPolicy, timeoutPolicy);

            services.AddHttpClient<IJsonPlaceholderService, JsonPlaceholderService>("JsonPlaceholder", client =>
            {
                client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
                client.Timeout = TimeSpan.FromSeconds(10);
            })
                .AddPolicyHandler(combinedPolicy);
        }
    }
}
