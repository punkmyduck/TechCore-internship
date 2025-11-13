namespace KafkaConsumerTest.Settings
{
    public class RedisSettings
    {
        public string Host { get; set; } = "localhost";
        public int Port { get; set; } = 6379;
        public string? Password { get; set; }
        public string ConnectionString => string.IsNullOrEmpty(Password)
            ? $"{Host}:{Port}"
            : $"{Host}:{Port},password={Password}";
    }
}
