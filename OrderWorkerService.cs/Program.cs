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
                    cfg.Host("localhost", "/", h =>
                    {
                        h.Username("rabbit");
                        h.Password("rabbitpass");
                    });
                    cfg.ReceiveEndpoint("submit-order-queue", e =>
                    {
                        e.ConfigureConsumer<SubmitOrderConsumer>(context);
                    });
                });
            });

            var host = builder.Build();
            host.Run();
        }
    }
}