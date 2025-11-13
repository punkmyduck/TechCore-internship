namespace KafkaConsumerTest.Settings
{
    public class RabbitMqSettings
    {
        public string Host { get; set; } = "rabbitmq";
        public int Port { get; set; } = 5672;
        public string VirtualHost { get; set; } = "/";
        public string Exchange { get; set; } = "test-exchange";
        public string Queue { get; set; } = "test-queue";
        public string RoutingKey { get; set; } = "test-key";
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}