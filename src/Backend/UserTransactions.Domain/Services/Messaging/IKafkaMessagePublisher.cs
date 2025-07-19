namespace UserTransactions.Domain.Services.Messaging
{
    public interface IKafkaMessageProducer
    {
        Task PublishAsync<T>(string topic, string key, T message) where T : class;
    }
}