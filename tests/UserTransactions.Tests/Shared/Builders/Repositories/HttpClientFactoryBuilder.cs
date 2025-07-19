using Moq;

namespace UserTransactions.Tests.Shared.Builders.Repositories
{
    public static class HttpClientFactoryBuilder
    {
        private static readonly Mock<IHttpClientFactory> _mock = new Mock<IHttpClientFactory>();

        public static IHttpClientFactory Build() => _mock.Object;

        public static void SetupCreateClient(HttpClient httpClient)
        {
            _mock.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);
        }

        public static void SetupCreateClient(string name, HttpClient httpClient)
        {
            _mock.Setup(x => x.CreateClient(name)).Returns(httpClient);
        }

        public static void VerifyCreateClientWasCalled()
        {
            _mock.Verify(x => x.CreateClient(It.IsAny<string>()), Times.AtLeastOnce);
        }

        public static void VerifyCreateClientWasCalled(string name)
        {
            _mock.Verify(x => x.CreateClient(name), Times.AtLeastOnce);
        }
    }
}