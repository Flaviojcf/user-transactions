using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using UserTransactions.Domain.Services.Messaging;

namespace UserTransactions.Infrastructure.Services.Messaging
{
    public class KafkaMessagePublisher : IKafkaMessagePublisher, IDisposable
    {
        private readonly IProducer<string, string> _producer;
        private readonly ILogger<KafkaMessagePublisher> _logger;

        public KafkaMessagePublisher(KafkaProducerFactory producerFactory, ILogger<KafkaMessagePublisher> logger)
        {
            _producer = producerFactory.CreateProducer();
            _logger = logger;
        }

        public async Task PublishAsync<T>(string topic, T message) where T : class
        {
            var serializedMessage = JsonSerializer.Serialize(message, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            var result = await _producer.ProduceAsync(topic, new Message<string, string>
            {
                Value = serializedMessage,
                Timestamp = new Timestamp(DateTime.UtcNow)
            });

            _logger.LogInformation($"Message delivered to {result.TopicPartitionOffset}");
        }

        public async Task PublishAsync<T>(string topic, string key, T message) where T : class
        {
            var serializedMessage = JsonSerializer.Serialize(message, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            var result = await _producer.ProduceAsync(topic, new Message<string, string>
            {
                Key = key,
                Value = serializedMessage,
                Timestamp = new Timestamp(DateTime.UtcNow)
            });

            _logger.LogInformation($"Message delivered to {result.TopicPartitionOffset}");
        }

        public void Dispose()
        {
            _producer?.Flush(TimeSpan.FromSeconds(10));
            _producer?.Dispose();
        }
    }
}