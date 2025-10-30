using Contracts;
using MassTransit;

namespace task1135.Extensions
{
    public static class RabbitMqExtensions
    {
        public static void AddRabbitMqMassTransit(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("rabbitmq", "/", h =>
                    {
                        h.Username("rabbit");
                        h.Password("rabbitpass");
                    });
                });
            });
        }
    }
}
