using Contracts;
using MassTransit;

namespace OrderWorkerService.cs.Workers
{
    public class SubmitOrderConsumer : IConsumer<SubmitOrderCommand>
    {
        private static int test = 0;
        private readonly ILogger<SubmitOrderCommand> _logger;
        private readonly IPublishEndpoint _publishEndpoint;
        public SubmitOrderConsumer(
            ILogger<SubmitOrderCommand> logger,
            IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<SubmitOrderCommand> context)
        {
            var msg = context.Message;

            _logger.LogInformation($"Order received: {msg.OrderId} with {msg.Items.Count} items");

            //if (test < 5)
            //{
            //    test++;
            //    throw new Exception("Simulate exception");
            //}

            //await Task.Delay(3000);

            await _publishEndpoint.Publish(new OrderCreatedEvent(msg.OrderId, DateTime.UtcNow));
        }
    }
}
