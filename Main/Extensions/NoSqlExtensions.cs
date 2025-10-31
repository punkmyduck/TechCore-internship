using MongoDB.Driver;
using task_1135.Application.Settings;

namespace task1135.Extensions
{
    public static class NoSqlExtensions
    {
        public static void AddNoSqlServices(this WebApplicationBuilder builder)
        {
            var mongoConfiguration = builder.Configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();
            var redisConfiguration = builder.Configuration.GetSection("RedisSettings").Get<RedisSettings>();

            if (mongoConfiguration == null) throw new ArgumentNullException("Can not load mongodb configuration");
            if (redisConfiguration == null) throw new ArgumentNullException("Can not load redis configuration");

            //MongoDB Configuration
            builder.Services.AddSingleton<IMongoClient>(sp =>
                new MongoClient(mongoConfiguration.ConnectionString));

            //Redis configuration
            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConfiguration.ConnectionString;
                options.InstanceName = redisConfiguration.InstanceName;
            });
        }
    }
}
