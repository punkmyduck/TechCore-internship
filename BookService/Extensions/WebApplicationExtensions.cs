using BookService.Infrastructure.Middlewares;

namespace BookService.Extensions
{
    public static class WebApplicationExtensions
    {
        public static void UseCustomMiddlewares(this WebApplication app)
        {
            app.UseMiddleware<TimingMiddleware>();
            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
