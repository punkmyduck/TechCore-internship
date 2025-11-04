using KafkaConsumerWorker.Extensions;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Confluent.Kafka.Extensions.OpenTelemetry;

namespace KafkaConsumerWorker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            builder.AddKafkaConsumer();
            builder.AddMongoDb();

            builder.Services.AddHostedService<KafkaConsumerService>();

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