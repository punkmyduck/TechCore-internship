using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
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

            //Redis configuration
            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "redis:6379";
                options.InstanceName = "booksapp_";
            });

            //Database configuration
            builder.Services.AddDbContext<BookContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            //Appsettings configuration
            builder.Services.Configure<MySettings>(builder.Configuration.GetSection("MySettings"));
            builder.Services.Configure<AsyncSettings>(builder.Configuration.GetSection("AsyncSettings"));

            // Register application services
            builder.Services.AddScoped<ITimeService, TimeService>();
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddSingleton<ILogService, ConsoleLogService>();
            builder.Services.AddScoped<IAuthorService, AuthorService>();
            builder.Services.AddScoped<IReportService, ReportService>();

            // Register infrastructure services
            builder.Services.AddSingleton<BookStorage>();
            builder.Services.AddScoped<IBookRepository, DatabaseBookRepository>();
            builder.Services.AddScoped<IAuthorRepository, DatabaseAuthorRepository>();

            //Register fluent validators
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddFluentValidationClientsideAdapters();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateBookDtoFluentValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<UpdateBookDtoFluentValidator>();


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

            app.MapControllers();

            app.MapHealthChecks("/healthz");

            app.Run();
        }
    }
}
