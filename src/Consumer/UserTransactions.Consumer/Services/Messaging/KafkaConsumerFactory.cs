using Confluent.Kafka;
using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;
using UserTransactions.Consumer.Configuration;

namespace UserTransactions.Consumer.Services.Messaging
{
    [ExcludeFromCodeCoverage]
    public class KafkaConsumerFactory
    {
        private readonly KafkaOptions _kafkaOptions;

        public KafkaConsumerFactory(IOptions<KafkaOptions> kafkaOptions)
        {
            _kafkaOptions = kafkaOptions.Value;
        }

        public IConsumer<string, string> CreateConsumer(string groupId)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = _kafkaOptions.BootstrapServers,
                SecurityProtocol = Enum.Parse<SecurityProtocol>(_kafkaOptions.SecurityProtocol),
                ClientId = _kafkaOptions.ClientId,
                GroupId = groupId,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = false,
                SessionTimeoutMs = 6000,
                AllowAutoCreateTopics = true
            };

            return new ConsumerBuilder<string, string>(config).Build();
        }
    }
}