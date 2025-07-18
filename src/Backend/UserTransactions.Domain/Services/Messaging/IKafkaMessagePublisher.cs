namespace UserTransactions.Domain.Services.Messaging
{
    public interface IKafkaMessagePublisher
    {
        Task PublishAsync<T>(string topic, T message) where T : class;
        Task PublishAsync<T>(string topic, string key, T message) where T : class;
    }
}