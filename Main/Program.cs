using task1135.Application.Services;
using task1135.Extensions;
using task1135.Infrastructure.Middlewares;

namespace Domain
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();

            // Add services to the container.
            builder.Services.AddAuthorizationWithPolicy();
            builder.Services.AddControllers();
            builder.Services.AddHealthChecks();

            //Add http clients
            builder.Services.AddJsonPlaceholderHttpClient();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddSwaggerUI();

            //rabbitmq configuration
            builder.Services.AddRabbitMqMassTransit();

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

            using (var scope = app.Services.CreateScope())
            {
                await scope.ServiceProvider.CreateRoleAsync("Admin");
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
