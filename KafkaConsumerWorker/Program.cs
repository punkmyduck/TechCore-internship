using KafkaConsumerWorker.Extensions;

namespace KafkaConsumerWorker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            builder.AddKafkaConsumer();

            builder.Services.AddHostedService<KafkaConsumerService>();

            var host = builder.Build();
            host.Run();
        }
    }
}