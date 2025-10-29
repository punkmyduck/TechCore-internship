using Polly;

namespace task_1135.Extensions
{
    public static class HttpClientExtensions
    {
        public static void AddJsonPlaceholderHttpClient(this IServiceCollection services)
        {
            services.AddHttpClient("JsonPlaceholder", client =>
            {
                client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
                client.Timeout = TimeSpan.FromSeconds(10);
            })
                .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(1)));
        }
    }
}
