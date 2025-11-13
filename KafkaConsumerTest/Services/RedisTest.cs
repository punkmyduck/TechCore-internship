using StackExchange.Redis;

namespace KafkaConsumerTest.Services
{
    public class RedisTest : BackgroundService
    {
        private readonly IConnectionMultiplexer _mux;
        private readonly ILogger<RedisTest> _logger;

        public RedisTest(IConnectionMultiplexer mux, ILogger<RedisTest> logger)
        {
            _mux = mux;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var db = _mux.GetDatabase();
            await db.StringSetAsync("hello", "world");
            var v = await db.StringGetAsync("hello");
            _logger.LogInformation("Redis hello -> {v}", v);
        }
    }
}
