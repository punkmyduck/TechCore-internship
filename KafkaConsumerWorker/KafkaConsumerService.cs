using System.Collections.Concurrent;
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

            var processingTasks = new ConcurrentBag<Task>();

            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    var result = _consumer.Consume(stoppingToken);
                    if (result?.Message == null) continue;

                    var task = Task.Run(async () =>
                    {
                        try
                        {
                            var doc = new BsonDocument
                            {
                                {"Key", result.Message.Key },
                                {"Value", result.Message.Value },
                                {"Timestamp", result.Message.Timestamp.UtcDateTime }
                            };

                            await _collection.InsertOneAsync(doc, cancellationToken: stoppingToken);
                            _consumer.StoreOffset(result);
                            _logger.LogInformation($"Received: {result.Message.Value}");
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Error processing message at offset {Offset}", result.Offset);
                        }
                    }, stoppingToken);

                    processingTasks.Add(task);
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("kafka consumer stopping...");
            }
            finally
            {
                await Task.WhenAll(processingTasks);
                _consumer.Close();
            }
        }
    }
}
