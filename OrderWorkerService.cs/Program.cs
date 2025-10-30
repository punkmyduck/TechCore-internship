using MassTransit;
using OrderWorkerService.cs.Workers;

namespace OrderWorkerService.cs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            builder.Services.AddMassTransit(x =>
            {
                x.AddConsumer<SubmitOrderConsumer>(cfg =>
                {
                    cfg.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5)));
                });

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("rabbitmq", "/", h =>
                    {
                        h.Username("rabbit");
                        h.Password("rabbitpass");
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