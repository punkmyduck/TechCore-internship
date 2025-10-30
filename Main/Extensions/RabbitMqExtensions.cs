using MassTransit;

namespace task_1135.Extensions
{
    public static class RabbitMqExtensions
    {
        public static void AddRabbitMqMassTransit(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", h =>
                    {
                        h.Username("rabbit");
                        h.Password("rabbitpass");
                    });
                });
            });
        }
    }
}
