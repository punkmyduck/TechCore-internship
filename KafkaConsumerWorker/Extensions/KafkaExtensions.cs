using Confluent.Kafka;
using KafkaConsumerWorker.Settings;

namespace KafkaConsumerWorker.Extensions
{
    public static class KafkaExtensions
    {
        public static void AddKafkaConsumer(this HostApplicationBuilder builder)
        {
            var kafkaConf = builder.Configuration.GetSection("KafkaSettings").Get<KafkaSettings>();
            if (kafkaConf == null) throw new ArgumentNullException("Can not to load kafka configuration");

            builder.Services.AddSingleton<IConsumer<string, string>>(sp =>
            {
                var conf = new ConsumerConfig
                {
                    BootstrapServers = kafkaConf.BootstrapServers,
                    GroupId = "analytics-consumer-group",
                    AutoOffsetReset = AutoOffsetReset.Earliest,
                    EnableAutoCommit = false,
                    EnableAutoOffsetStore = false
                };

                var consumer = new ConsumerBuilder<string, string>(conf).Build();
                consumer.Subscribe(kafkaConf.Topic);

                return consumer;
            });
        }
    }
}
