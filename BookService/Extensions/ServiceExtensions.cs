using BookService.Application.BackgroundServices;
using BookService.Application.Services;
using BookService.Application.Validators;
using Domain.Repositories;
using Domain.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Persistence.Infrastructure.Repositories;

namespace BookService.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ITimeService, TimeService>();
            services.AddScoped<IBookService, Application.Services.BookService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IProductReviewService, ProductReviewService>();
            services.AddScoped<IJwtService, JwtService>();
        }

        public static void AddValidatorsServices(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<CreateBookDtoFluentValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateBookDtoFluentValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateReviewDtoFluentValidator>();
        }

        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddScoped<IBookRepository, DatabaseBookRepository>();
            services.AddScoped<IAuthorRepository, DatabaseAuthorRepository>();
            services.AddScoped<IProductReviewRepository, ProductReviewRepository>();
        }

        public static void AddBackgroundServices(this IServiceCollection services)
        {
            services.AddHostedService<AverageRatingCalculatorService>();
        }
    }
}
