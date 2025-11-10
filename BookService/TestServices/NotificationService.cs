using Contracts;
using MassTransit;

namespace BookService.TestServices
{
    public class NotificationService : IConsumer<OrderCreatedEvent>
    {
        public Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            Console.WriteLine($"[Notification] OrderCreatedEvent received, notification sent to somebody idk...");
            return Task.CompletedTask;
        }
    }
}
