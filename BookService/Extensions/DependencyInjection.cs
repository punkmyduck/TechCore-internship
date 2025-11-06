using Domain.Repositories;
using Domain.Services;
using Persistence.Infrastructure.Repositories;

namespace BookService.Extensions
{
    public static class DependencyInjection
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IBookService, Services.BookService>();
            services.AddScoped<IBookRepository, DatabaseBookRepository>();
        }
    }
}
