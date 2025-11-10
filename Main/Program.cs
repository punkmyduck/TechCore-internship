using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Persistence.Extensions;
using task_1135.Extensions;
using task1135.Extensions;
using task1135.Infrastructure.Middlewares;
using Confluent.Kafka.Extensions.OpenTelemetry;
using OpenTelemetry.Metrics;
using Serilog;
using Serilog.Sinks.Grafana.Loki;
using task_1135.Application.BackgroundServices;

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
            builder.Services.AddHostedService<AverageRatingCalculatorService>();

            //NoSQL configuration
            builder.AddNoSqlServices();

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

            //JWT configuration
            builder.AddJwtAuthentication();

            //opentelemetry configuration
            builder.Services.AddOpenTelemetry()
                .WithTracing(b =>
                {
                    b
                    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(builder.Environment.ApplicationName))
                    .AddHttpClientInstrumentation()
                    .AddAspNetCoreInstrumentation()
                    .AddConfluentKafkaInstrumentation()
                    .AddMassTransitInstrumentation()
                    .AddZipkinExporter(o =>
                    {
                        o.Endpoint = new Uri(builder.Configuration.GetSection("ZipkinSettings")["Path"]!);
                    });
                });

            builder.Services.AddOpenTelemetry()
                .WithMetrics(b =>
                {
                    b
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddPrometheusExporter();
                });

            builder.Services.AddOpenTelemetry()
                .WithMetrics(b =>
                {
                    b.AddMeter("BookService.Metrics")
                    .AddPrometheusExporter();
                });


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

            app.UseMiddleware<TimingMiddleware>();
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseOutputCache();

            app.MapControllers();

            app.MapHealthChecks("/healthz");
            app.MapPrometheusScrapingEndpoint("/metrics");

            app.Run();
        }
    }
}
