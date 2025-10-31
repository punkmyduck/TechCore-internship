using Confluent.Kafka;
using Domain.Services;

namespace task_1135.Application.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IProducer<string, string> _producer;
        public AnalyticsService(IProducer<string, string> producer)
        {
            _producer = producer;
        }


    }
}
