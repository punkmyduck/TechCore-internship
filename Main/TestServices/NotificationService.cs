using Contracts;
using MassTransit;

namespace task_1135.TestServices
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
