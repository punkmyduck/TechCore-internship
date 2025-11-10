using Confluent.Kafka;
using BookService.Application.Settings;

namespace BookService.Extensions
{
    public static class KafkaExtensions
    {
        public static void AddKafkaProducer(this WebApplicationBuilder builder)
        {
            var kafkaConf = builder.Configuration.GetSection("KafkaSettings").Get<KafkaSettings>();
            if (kafkaConf == null) throw new ArgumentNullException("Can not to load kafka configuration");

            builder.Services.AddSingleton<IProducer<string, string>>(sp =>
            {
                var config = new ProducerConfig
                {
                    BootstrapServers = kafkaConf.BootstrapServers
                };
                return new ProducerBuilder<string, string>(config)
                .Build();
            });
        }
    }
}
