using Domain.Repositories;
using Domain.Services;
using Persistence.Infrastructure.Repositories;
using BookServiceDemo.Services;

namespace BookServiceDemo.Extensions
{
    public static class DependencyInjection
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IBookRepository, DatabaseBookRepository>();
        }
    }
}
