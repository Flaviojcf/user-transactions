using UserTransactions.Communication.Dtos.Wallet.Request;

namespace UserTransactions.Tests.Shared.Builders.Dtos.Request.Wallet
{
    public static class RequestCreateWalletDtoBuilder
    {
        public static RequestCreateWalletDto Build()
        {
            return new RequestCreateWalletDto
            {
                UserId = Guid.NewGuid(),
            };
        }
    }
}
