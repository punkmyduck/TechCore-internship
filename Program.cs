using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using task_1135.Application.Services;
using task_1135.Application.Settings;
using task_1135.Application.Validators;
using task_1135.Domain.Repositories;
using task_1135.Domain.Services;
using task_1135.Infrastructure;
using task_1135.Infrastructure.Middlewares;
using task_1135.Infrastructure.Repositories;
using task_1135.Infrastructure.Storage;

namespace task_1135
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddHealthChecks();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
            });

            //HostedServices configuration
            builder.Services.AddHostedService<AverageRatingCalculatorService>();

            //MongoDB Configuration
            builder.Services.AddSingleton<IMongoClient>(sp =>
                new MongoClient("mongodb://localhost:27017"));

            //Redis configuration
            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost:6379";
                options.InstanceName = "booksapp_";
            });

            //OutputCache configuration
            builder.Services.AddOutputCache(options =>
            {
                options.AddPolicy("BookPolicy", policy =>
                policy.Cache().Expire(TimeSpan.FromSeconds(60)));
            });

            //Database configuration
            builder.Services.AddDbContext<BookContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            //Appsettings configuration
            builder.Services.Configure<MySettings>(builder.Configuration.GetSection("MySettings"));
            builder.Services.Configure<AsyncSettings>(builder.Configuration.GetSection("AsyncSettings"));

            // Register application services
            builder.Services.AddScoped<ITimeService, TimeService>();
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<IAuthorService, AuthorService>();
            builder.Services.AddScoped<IReportService, ReportService>();
            builder.Services.AddScoped<IProductReviewService, ProductReviewService>();

            // Register infrastructure services
            builder.Services.AddSingleton<BookStorage>();
            builder.Services.AddScoped<IBookRepository, DatabaseBookRepository>();
            builder.Services.AddScoped<IAuthorRepository, DatabaseAuthorRepository>();
            builder.Services.AddScoped<IProductReviewRepository, ProductReviewRepository>();

            //Register fluent validators
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddFluentValidationClientsideAdapters();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateBookDtoFluentValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<UpdateBookDtoFluentValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateReviewDtoFluentValidator>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseMiddleware<TimingMiddleware>();
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseOutputCache();

            app.MapControllers();

            app.MapHealthChecks("/healthz");

            app.Run();
        }
    }
}
