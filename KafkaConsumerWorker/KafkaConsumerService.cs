using Confluent.Kafka;

namespace KafkaConsumerWorker
{
    public class KafkaConsumerService : BackgroundService
    {
        private readonly ILogger<KafkaConsumerService> _logger;
        private readonly IConsumer<string, string> _consumer;

        public KafkaConsumerService(
            ILogger<KafkaConsumerService> logger,
            IConsumer<string, string> consumer)
        {
            _logger = logger;
            _consumer = consumer;
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
                    await Task.Delay(100, stoppingToken);
                }
            }
            finally
            {
                _consumer.Close();
            }
        }
    }
}
