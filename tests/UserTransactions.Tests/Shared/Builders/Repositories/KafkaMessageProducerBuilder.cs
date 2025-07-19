using Moq;
using UserTransactions.Domain.Services.Messaging;

namespace UserTransactions.Tests.Shared.Builders.Repositories
{
    public static class KafkaMessageProducerBuilder
    {
        private static readonly Mock<IKafkaMessageProducer> _mock = new Mock<IKafkaMessageProducer>();

        public static IKafkaMessageProducer Build() => _mock.Object;

        public static void SetupPublishAsync<T>() where T : class
        {
            _mock.Setup(x => x.PublishAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<T>()))
                .Returns(Task.CompletedTask);
        }
    }
}