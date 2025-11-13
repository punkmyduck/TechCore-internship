using MassTransit;
using KafkaConsumerTest.Models;

namespace KafkaConsumerTest.Services
{
    public class RabbitMqTestService : BackgroundService
    {
        private readonly ILogger<RabbitMqTestService> _logger;
        private readonly IBus _bus;
        private readonly Random rnd;
        private static int _counter = 0;
        public RabbitMqTestService(IBus bus, ILogger<RabbitMqTestService> logger)
        {
            _bus = bus;
            rnd = new Random();
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    await Task.Delay(20000);
                    _logger.LogInformation("Trying to send something to rabbitmq bus");
                    var s = new SomeThing(_counter++, $"Some string {rnd.Next()}");
                    await _bus.Publish(s);
                    _logger.LogInformation("Sent {@SomeThing} to rabbitmq bus", s);
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("rabbitmq sender stopping...");
            }
            finally
            {

            }
        }
    }
}