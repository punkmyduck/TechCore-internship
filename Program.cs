using System.Reflection;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using task_1135.Application.Services;
using task_1135.Application.Settings;
using task_1135.Application.Validators;
using task_1135.Domain.Repositories;
using task_1135.Domain.Services;
using task_1135.Extensions;
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
            builder.Services.AddAuthorization();
            builder.Services.AddControllers();
            builder.Services.AddHealthChecks();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddSwaggerUI();

            //HostedServices configuration
            builder.Services.AddHostedService<AverageRatingCalculatorService>();

            //NoSQL configuration
            builder.Services.AddNoSqlServices();

            //OutputCache configuration
            builder.Services.AddOutputCache(options =>
            {
                options.AddPolicy("BookPolicy", policy =>
                policy.Cache().Expire(TimeSpan.FromSeconds(60)));
            });

            //Database configuration
            builder.AddDatabaseConfiguration();

            //Appsettings configuration
            builder.AddAppSettingsConfiguration();

            // Register application services
            builder.Services.AddApplicationServices();

            //JWT configuration
            builder.AddJwtAuthentication();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
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
