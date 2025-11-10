using MassTransit;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OrderWorkerService.Workers;
using Confluent.Kafka.Extensions.OpenTelemetry;
using OrderWorkerService.Application.Settings;

namespace OrderWorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            var rabbitMqSettings = builder.Configuration.GetSection("RabbitMQ").Get<RabbitMqSettings>();
            if (rabbitMqSettings == null) throw new ArgumentNullException("Can not to load rabbitmq configuration");

            builder.Services.AddMassTransit(x =>
            {
                x.AddConsumer<SubmitOrderConsumer>(cfg =>
                {
                    cfg.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5)));
                });

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(rabbitMqSettings.Host, "/", h =>
                    {
                        h.Username(rabbitMqSettings.Username);
                        h.Password(rabbitMqSettings.Password);
                    });
                    cfg.ReceiveEndpoint("submit-order-queue", e =>
                    {
                        e.ConfigureConsumer<SubmitOrderConsumer>(context);

                        e.UseMessageRetry(r=> r.Interval(3, TimeSpan.FromSeconds(5)));
                    });
                });
            });

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

            var host = builder.Build();
            host.Run();
        }
    }
}