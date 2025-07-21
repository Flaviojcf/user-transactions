using System.Diagnostics.CodeAnalysis;

namespace UserTransactions.Consumer.Configuration
{
    [ExcludeFromCodeCoverage]
    public class KafkaOptions
    {
        public const string SectionName = "Kafka";

        public string BootstrapServers { get; set; } = string.Empty;
        public string SecurityProtocol { get; set; } = "Plaintext";
        public string SaslMechanism { get; set; } = string.Empty;
        public string SaslUsername { get; set; } = string.Empty;
        public string SaslPassword { get; set; } = string.Empty;
        public int MessageTimeoutMs { get; set; } = 5000;
        public int RequestTimeoutMs { get; set; } = 30000;
        public string ClientId { get; set; } = "UserTransactions";
        public bool EnableIdempotence { get; set; } = true;
        public string Acks { get; set; } = "all";
    }
}