using Contracts;
using MassTransit;

namespace OrderWorkerService.cs.Workers
{
    public class SubmitOrderConsumer : IConsumer<SubmitOrderCommand>
    {
        private readonly ILogger<SubmitOrderCommand> _logger;
        public SubmitOrderConsumer(ILogger<SubmitOrderCommand> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<SubmitOrderCommand> context)
        {
            var msg = context.Message;
            _logger.LogInformation($"Order received: {msg.OrderId} with {msg.Items.Count} items");
            return Task.CompletedTask;
        }
    }
}
