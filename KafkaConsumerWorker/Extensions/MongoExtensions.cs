using KafkaConsumerWorker.Settings;
using MongoDB.Driver;

namespace KafkaConsumerWorker.Extensions
{
    public static class MongoExtensions
    {
        public static void AddMongoDb(this HostApplicationBuilder builder)
        {
            var mongoConfiguration = builder.Configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();

            if (mongoConfiguration == null) throw new ArgumentNullException("Can not load mongodb configuration");

            builder.Services.AddSingleton<IMongoClient>(sp =>
                new MongoClient(mongoConfiguration.ConnectionString));
        }
    }
}
