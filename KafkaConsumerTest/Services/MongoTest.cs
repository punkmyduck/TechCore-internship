using MongoDB.Driver;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using KafkaConsumerTest.Models;

namespace KafkaConsumerTest.Services
{
    public class MongoTest : BackgroundService
    {
        private readonly IMongoCollection<RealThing> _collection;
        private readonly ILogger<MongoTest> _logger;

        public MongoTest(IMongoClient client, ILogger<MongoTest> logger)
        {
            _logger = logger;
            var db = client.GetDatabase("appdb");
            _collection = db.GetCollection<RealThing>("real_things");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Trying to start mongo...");
            try
            {
                var doc = new RealThing { IsThisReal = true, SomeString = "Some string" };
                await _collection.InsertOneAsync(doc, cancellationToken: stoppingToken);

                var thing = await _collection.Find(r => r.SomeString == "Some string")
                                             .FirstOrDefaultAsync(cancellationToken: stoppingToken);

                _logger.LogInformation("Inserted document id: {Id}", thing?.Id ?? "<null>");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while inserting/reading Mongo document");
                // Если необходимо — пробросьте исключение, чтобы контейнер перезапустился:
                // throw;
            }
        }
    }
}
