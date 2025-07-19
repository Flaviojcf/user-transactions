using System.Net;

namespace UserTransactions.Tests.Shared.Mocks
{
    public class MockHttpMessageHandler : HttpMessageHandler
    {
        private HttpStatusCode _statusCode = HttpStatusCode.OK;

        public void SetupSuccessResponse()
        {
            _statusCode = HttpStatusCode.OK;
        }

        public void SetupFailureResponse()
        {
            _statusCode = HttpStatusCode.Forbidden;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            await Task.Delay(1, cancellationToken);
            return new HttpResponseMessage(_statusCode);
        }
    }
}