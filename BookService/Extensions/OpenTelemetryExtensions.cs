using Confluent.Kafka.Extensions.OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace BookService.Extensions
{
    public static class OpenTelemetryExtensions
    {
        public static void AddOpenTelemetryTracing(this WebApplicationBuilder builder)
        {
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
        }

        public static void AddPrometheusTracing(this WebApplicationBuilder builder)
        {
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
        }
    }
}
