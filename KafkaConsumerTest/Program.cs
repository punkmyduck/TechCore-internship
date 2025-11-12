using Confluent.Kafka;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace KafkaConsumerTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            var kafkaConf = builder.Configuration.GetSection("KafkaSettings").Get<KafkaSettings>();
            if (kafkaConf == null) throw new ArgumentNullException("Can not to load kafka configuration");

            builder.Services.AddSingleton<IConsumer<string, string>>(sp =>
            {
                var conf = new ConsumerConfig
                {
                    BootstrapServers = kafkaConf.BootstrapServers,
                    GroupId = "bebra-group",
                    AutoOffsetReset = AutoOffsetReset.Earliest,
                    EnableAutoCommit = false,
                    EnableAutoOffsetStore = false
                };

                var consumer = new ConsumerBuilder<string, string>(conf).Build();
                consumer.Subscribe(kafkaConf.Topic);

                return consumer;
            });

            builder.Services.AddHostedService<TestService>();

            // ---- fix: use the same section name "MongoSettings" ----
            builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection("MongoSettings"));
            var mongoSettings = builder.Configuration.GetSection("MongoSettings").Get<MongoSettings>();
            if (mongoSettings == null) throw new ArgumentNullException("MongoSettings not found in configuration");
            builder.Services.AddSingleton(mongoSettings);

            builder.Services.AddSingleton<IMongoClient>(sp =>
            {
                var cfg = sp.GetRequiredService<MongoSettings>();
                return new MongoClient(cfg.ConnectionString);
            });

            builder.Services.AddHostedService<MongoTest>();

            var host = builder.Build();
            host.Run();
        }
    }
}