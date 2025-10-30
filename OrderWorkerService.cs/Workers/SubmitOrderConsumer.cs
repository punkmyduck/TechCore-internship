using Contracts;
using MassTransit;

namespace OrderWorkerService.cs.Workers
{
    public class SubmitOrderConsumer : IConsumer<SubmitOrderCommand>
    {
        private static int test = 0;
        private readonly ILogger<SubmitOrderCommand> _logger;
        public SubmitOrderConsumer(ILogger<SubmitOrderCommand> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<SubmitOrderCommand> context)
        {
            var msg = context.Message;

            _logger.LogInformation($"Order received: {msg.OrderId} with {msg.Items.Count} items");

            if (test < 2)
            {
                test++;
                throw new Exception("Simulate exception");
            }

            await Task.Delay(3000);
        }
    }
}
