using Persistence.Extensions;
using BookService.Extensions;
using Serilog;
using Serilog.Sinks.Grafana.Loki;

namespace BookService
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.GrafanaLoki("http://loki:3100")
                .CreateLogger();

            builder.Host.UseSerilog();

            // Add services to the container.
            builder.Services.AddAuthorizationWithPolicy();
            builder.Services.AddControllers();
            builder.Services.AddHealthChecks();

            //Add http clients
            builder.Services.AddJsonPlaceholderHttpClient();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddSwaggerUI();

            //rabbitmq configuration
            builder.AddRabbitMqMassTransit();

            //HostedServices configuration
            builder.Services.AddBackgroundServices();

            //NoSQL configuration
            builder.AddRedisService();
            builder.AddMongoDbService();

            //Kafka configuration
            builder.AddKafkaProducer();

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
            builder.Services.AddPersistenceServices();
            builder.Services.AddValidatorsServices();

            //JWT configuration
            builder.AddJwtAuthentication();

            //opentelemetry configuration
            builder.AddOpenTelemetryTracing();
            builder.AddPrometheusTracing();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment() || true)
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

            app.UseCustomMiddlewares();
            app.UseOutputCache();

            app.MapControllers();

            app.MapHealthChecks("/healthz");
            app.MapPrometheusScrapingEndpoint("/metrics");

            app.Run();
        }
    }
}
