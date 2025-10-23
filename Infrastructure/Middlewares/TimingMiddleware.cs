using task_1135.Domain.Services;

namespace task_1135.Infrastructure.Middlewares
{
    public class TimingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogService _logService;
        public TimingMiddleware(
            RequestDelegate next,
            ILogService logService)
        {
            _next = next;
            _logService = logService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _logService.Log($"Current request:\n{context.Request.Path}/{context.Request.Method}");
            var startTime = DateTime.UtcNow;
            await _next.Invoke(context);
            var endTime = DateTime.UtcNow;
            var duration = endTime - startTime;
            _logService.Log("Execution time: " + duration.TotalMilliseconds);
        }
    }
}
