using Confluent.Kafka;
using MongoDB.Bson;
using MongoDB.Driver;

namespace KafkaConsumerWorker
{
    public class KafkaConsumerService : BackgroundService
    {
        private readonly ILogger<KafkaConsumerService> _logger;
        private readonly IConsumer<string, string> _consumer;
        private readonly IMongoCollection<BsonDocument> _collection;

        public KafkaConsumerService(
            ILogger<KafkaConsumerService> logger,
            IConsumer<string, string> consumer,
            IMongoClient mongoClient)
        {
            _logger = logger;
            _consumer = consumer;
            _collection = mongoClient.GetDatabase("analyticsdb").GetCollection<BsonDocument>("events");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Kafka consumer started");

            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    var result = _consumer.Consume(stoppingToken);
                    _logger.LogInformation($"Received: {result.Message.Value}");

                    var doc = new BsonDocument
                    {
                        {"Key", result.Message.Key },
                        {"Value", result.Message.Value },
                        {"Timestamp", result.Message.Timestamp.UtcDateTime }
                    };

                    await _collection.InsertOneAsync(doc, cancellationToken: stoppingToken);
                }
            }
            finally
            {
                _consumer.Close();
            }
        }
    }
}
