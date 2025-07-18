using Confluent.Kafka;
using Microsoft.Extensions.Options;
using UserTransactions.Infrastructure.Configuration;

namespace UserTransactions.Infrastructure.Services.Messaging
{
    public class KafkaProducerFactory
    {
        private readonly KafkaOptions _kafkaOptions;

        public KafkaProducerFactory(IOptions<KafkaOptions> kafkaOptions)
        {
            _kafkaOptions = kafkaOptions.Value;
        }

        public IProducer<string, string> CreateProducer()
        {
            var config = new ProducerConfig
            {
                BootstrapServers = _kafkaOptions.BootstrapServers,
                SecurityProtocol = Enum.Parse<SecurityProtocol>(_kafkaOptions.SecurityProtocol),
                ClientId = _kafkaOptions.ClientId,
                EnableIdempotence = _kafkaOptions.EnableIdempotence,
                Acks = Enum.Parse<Acks>(_kafkaOptions.Acks),
                MessageTimeoutMs = _kafkaOptions.MessageTimeoutMs,
                RequestTimeoutMs = _kafkaOptions.RequestTimeoutMs,
            };

            if (!string.IsNullOrEmpty(_kafkaOptions.SaslMechanism))
            {
                config.SaslMechanism = Enum.Parse<SaslMechanism>(_kafkaOptions.SaslMechanism);
                config.SaslUsername = _kafkaOptions.SaslUsername;
                config.SaslPassword = _kafkaOptions.SaslPassword;
            }

            return new ProducerBuilder<string, string>(config).Build();
        }
    }
}