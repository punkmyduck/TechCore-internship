using MassTransit;
using BookService.Application.Settings;
using BookService.TestServices;

namespace BookService.Extensions
{
    public static class RabbitMqExtensions
    {
        public static void AddRabbitMqMassTransit(this WebApplicationBuilder builder)
        {
            var rabbitMqOptions = builder.Configuration.GetSection("RabbitMQ").Get<RabbitMqSettings>();
            if (rabbitMqOptions == null) throw new ArgumentNullException("Can not load configuration for rabbitmq");

            builder.Services.AddMassTransit(x =>
            {
                x.AddConsumer<NotificationService>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(rabbitMqOptions.Host, "/", h =>
                    {
                        h.Username(rabbitMqOptions.Username);
                        h.Password(rabbitMqOptions.Password);
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
