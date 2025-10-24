using Microsoft.AspNetCore.Mvc;
using task_1135.Domain.Services;

namespace task_1135.Infrastructure.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogService _logService;
        public ExceptionHandlerMiddleware(
            RequestDelegate next,
            ILogService logService)
        {
            _next = next;
            _logService = logService;
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

                _logService.Log(ex.Message + "\n" + ex.Source + "\n" + ex.StackTrace + "\n" + ex.InnerException);
                await context.Response.WriteAsJsonAsync(problem);
            }
        }
    }
}
