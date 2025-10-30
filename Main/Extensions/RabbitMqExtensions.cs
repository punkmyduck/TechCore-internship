using Contracts;
using MassTransit;
using task_1135.TestServices;

namespace task1135.Extensions
{
    public static class RabbitMqExtensions
    {
        public static void AddRabbitMqMassTransit(this IServiceCollection services)
        {

            services.AddMassTransit(x =>
            {
                x.AddConsumer<NotificationService>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("rabbitmq", "/", h =>
                    {
                        h.Username("rabbit");
                        h.Password("rabbitpass");
                    });

                    cfg.ReceiveEndpoint("notification-service", e =>
                    {
                        e.ConfigureConsumer<NotificationService>(context);
                    });
                });
            });
        }
    }
}
