using Bogus;
using UserTransactions.Communication.Dtos.Wallet.Response;

namespace UserTransactions.Tests.Shared.Builders.Dtos.Response.Wallet
{
    public static class ResponseListTotalQuantityWalletDtoBuilder
    {
        public static ResponseListTotalQuantityWalletDto Build()
        {
            return new Faker<ResponseListTotalQuantityWalletDto>()
                .RuleFor(x => x.TotalQuantity, f => f.Random.Int(1, 100))
                .Generate();
        }
    }
}