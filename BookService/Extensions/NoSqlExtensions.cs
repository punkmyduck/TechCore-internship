using MongoDB.Driver;
using BookService.Application.Settings;

namespace BookService.Extensions
{
    public static class NoSqlExtensions
    {
        public static void AddMongoDbService(this WebApplicationBuilder builder)
        {
            var mongoConfiguration = builder.Configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();
            if (mongoConfiguration == null) throw new ArgumentNullException("Can not load mongodb configuration");

            builder.Services.AddSingleton<IMongoClient>(sp =>
                new MongoClient(mongoConfiguration.ConnectionString));
        }

        public static void AddRedisService(this WebApplicationBuilder builder)
        {
            var redisConfiguration = builder.Configuration.GetSection("RedisSettings").Get<RedisSettings>();
            if (redisConfiguration == null) throw new ArgumentNullException("Can not load redis configuration");
            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConfiguration.ConnectionString;
                options.InstanceName = redisConfiguration.InstanceName;
            });
        }
    }
}
