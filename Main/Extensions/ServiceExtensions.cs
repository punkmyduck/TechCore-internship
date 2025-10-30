using FluentValidation;
using FluentValidation.AspNetCore;
using task1135.Application.Services;
using task1135.Application.Validators;
using Domain.Repositories;
using Domain.Services;
using task1135.Infrastructure.Repositories;
using task1135.Infrastructure.Storage;

namespace task1135.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ITimeService, TimeService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IProductReviewService, ProductReviewService>();
            services.AddScoped<IJwtService, JwtService>();
            //services.AddScoped<IJsonPlaceholderService, JsonPlaceholderService>();

            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<CreateBookDtoFluentValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateBookDtoFluentValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateReviewDtoFluentValidator>();

            services.AddSingleton<BookStorage>();
            services.AddScoped<IBookRepository, DatabaseBookRepository>();
            services.AddScoped<IAuthorRepository, DatabaseAuthorRepository>();
            services.AddScoped<IProductReviewRepository, ProductReviewRepository>();
        }
    }
}
