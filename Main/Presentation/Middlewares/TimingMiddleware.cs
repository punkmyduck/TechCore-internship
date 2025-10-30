namespace task1135.Infrastructure.Middlewares
{
    public class TimingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<TimingMiddleware> _logger;
        public TimingMiddleware(
            RequestDelegate next,
            ILogger<TimingMiddleware> logService)
        {
            _next = next;
            _logger = logService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogInformation($"Current request: {context.Request.Path}/{context.Request.Method}");
            var startTime = DateTime.UtcNow;
            await _next.Invoke(context);
            var endTime = DateTime.UtcNow;
            var duration = endTime - startTime;
            _logger.LogInformation("Execution time: " + duration.TotalMilliseconds);
        }
    }
}
