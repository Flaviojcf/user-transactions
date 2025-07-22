using FluentAssertions;
using Moq;
using Moq.Protected;
using System.Net;
using UserTransactions.Exception.Exceptions;
using UserTransactions.Infrastructure.Services.Authorize;
using UserTransactions.Tests.Shared.Builders.Repositories;

namespace UserTransactions.Tests.Infra.Services
{
    public class AuthorizeServiceTest
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly Mock<HttpMessageHandler> _httpMessageHandler;
        private readonly HttpClient _httpClient;
        private readonly AuthorizeService _sut;

        public AuthorizeServiceTest()
        {
            _httpClientFactory = HttpClientFactoryBuilder.Build();
            _httpMessageHandler = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_httpMessageHandler.Object);
            _sut = new AuthorizeService(_httpClientFactory);
        }

        [Fact]
        public async Task Given_AuthorizeServiceReturnsSuccess_When_ValidateAuthorizeServiceIsCalled_Then_ShouldCompleteSuccessfully()
        {
            // Arrange
            HttpClientFactoryBuilder.SetupCreateClient(_httpClient);

            _httpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));

            // Act
            Func<Task> act = async () => await _sut.ValidateAuthorizeService();

            // Assert
            await act.Should().NotThrowAsync();
        }

        [Theory]
        [InlineData(HttpStatusCode.BadRequest)]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.NotFound)]
        [InlineData(HttpStatusCode.InternalServerError)]
        public async Task Given_AuthorizeServiceReturnsError_When_ValidateAuthorizeServiceIsCalled_Then_ShouldThrowErrorOnValidationException(HttpStatusCode httpStatusCode)
        {
            // Arrange
            HttpClientFactoryBuilder.SetupCreateClient(_httpClient);

            _httpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage(httpStatusCode));

            // Act
            Func<Task> act = async () => await _sut.ValidateAuthorizeService();

            // Assert
            await act.Should().ThrowAsync<ErrorOnValidationException>();
        }
    }
}
