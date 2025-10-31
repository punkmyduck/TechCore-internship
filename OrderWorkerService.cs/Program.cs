using MassTransit;
using OrderWorkerService.cs.Workers;
using task_1135.Application.Settings;

namespace OrderWorkerService.cs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            var rabbitMqSettings = builder.Configuration.GetSection("RabbitMQ").Get<RabbitMqSettings>();
            if (rabbitMqSettings == null) throw new ArgumentNullException("Can not to load rabbitmq configuration");

            builder.Services.AddMassTransit(x =>
            {
                x.AddConsumer<SubmitOrderConsumer>(cfg =>
                {
                    cfg.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5)));
                });

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(rabbitMqSettings.Host, "/", h =>
                    {
                        h.Username(rabbitMqSettings.Username);
                        h.Password(rabbitMqSettings.Password);
                    });
                    cfg.ReceiveEndpoint("submit-order-queue", e =>
                    {
                        e.ConfigureConsumer<SubmitOrderConsumer>(context);

                        e.UseMessageRetry(r=> r.Interval(3, TimeSpan.FromSeconds(5)));
                    });
                });
            });

            var host = builder.Build();
            host.Run();
        }
    }
}