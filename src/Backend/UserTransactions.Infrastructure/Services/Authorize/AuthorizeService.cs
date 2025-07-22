using UserTransactions.Domain.Services.Authorize;
using UserTransactions.Exception;
using UserTransactions.Exception.Exceptions;

namespace UserTransactions.Infrastructure.Services.Authorize
{
    public class AuthorizeService : IAuthorizeService
    {

        private readonly IHttpClientFactory _httpClientFactory;
        private const string _authorizeUrl = "https://util.devi.tools/api/v2/authorize";

        public AuthorizeService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task ValidateAuthorizeService()
        {
            using var httpClient = _httpClientFactory.CreateClient();

            var response = await httpClient.GetAsync(_authorizeUrl);

            if (!response.IsSuccessStatusCode)
            {
                throw new ErrorOnAuthorizeException([ResourceMessagesException.TransactionNotAuthorized]);
            }
        }
    }
}
