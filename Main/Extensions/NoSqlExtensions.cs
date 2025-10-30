using MongoDB.Driver;

namespace task1135.Extensions
{
    public static class NoSqlExtensions
    {
        public static void AddNoSqlServices(this IServiceCollection services)
        {
            //MongoDB Configuration
            services.AddSingleton<IMongoClient>(sp =>
                new MongoClient("mongodb://localhost:27017"));

            //Redis configuration
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost:6379";
                options.InstanceName = "booksapp_";
            });
        }
    }
}
