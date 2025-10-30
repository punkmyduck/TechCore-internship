using MongoDB.Driver;

namespace task1135.Extensions
{
    public static class NoSqlExtensions
    {
        public static void AddNoSqlServices(this IServiceCollection services)
        {
            //MongoDB Configuration
            services.AddSingleton<IMongoClient>(sp =>
                new MongoClient("mongodb://mongo:27017"));

            //Redis configuration
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "redis:6379";
                options.InstanceName = "booksapp_";
            });
        }
    }
}
