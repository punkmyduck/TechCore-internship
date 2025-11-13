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

            var host = builder.Build();
            host.Run();
        }
    }
}