using Microsoft.AspNetCore.Mvc;

namespace BookService.Infrastructure.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        public ExceptionHandlerMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlerMiddleware> logService)
        {
            _next = next;
            _logger = logService;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/problem+json";

                var problem = new ProblemDetails
                {
                    Status = 500,
                    Title = "Internal server error",
                    Detail = ex.Message,
                    Instance = context.Request.Path
                };

                _logger.LogError(ex.Message + "\n" + ex.Source + "\n" + ex.StackTrace + "\n" + ex.InnerException);
                await context.Response.WriteAsJsonAsync(problem);
            }
        }
    }
}
