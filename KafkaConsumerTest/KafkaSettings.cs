namespace KafkaConsumerTest
{
    public class KafkaSettings
    {
        public string BootstrapServers { get; set; } = string.Empty;
        public string Topic { get; set; } = string.Empty;
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
