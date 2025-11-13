using KafkaConsumerTest.Models;
using MassTransit;

namespace KafkaConsumerTest.Services
{
    public class RabbitMqConsumerTest : IConsumer<SomeThing>
    {
        private readonly ILogger<RabbitMqConsumerTest> _logger;
        private readonly IPublishEndpoint _publishEndpoint;
        public RabbitMqConsumerTest(
            ILogger<RabbitMqConsumerTest> logger,
            IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }
        public async Task Consume(ConsumeContext<SomeThing> context)
        {
            var msg = context.Message;
            _logger.LogInformation($"Somethins received: {msg.Id} {msg.SomeString}");
        }
    }
}
