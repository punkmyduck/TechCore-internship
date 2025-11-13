using Confluent.Kafka;

namespace KafkaConsumerTest.Services
{
    public class TestService : BackgroundService
    {
        private readonly ILogger<TestService> _logger;
        private readonly IConsumer<string, string> _consumer;

        public TestService(
            ILogger<TestService> logger,
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
                    await Task.Delay(10000);
                    _logger.LogInformation($"Trying to receive something...");
                    var result = _consumer.Consume(stoppingToken);
                    if (result?.Message == null) continue;
                    _logger.LogInformation($"Received: {result.Message.Value}");
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("kafka consumer stopping...");
            }
            finally
            {
                _consumer.Close();
            }
        }
    }
}
