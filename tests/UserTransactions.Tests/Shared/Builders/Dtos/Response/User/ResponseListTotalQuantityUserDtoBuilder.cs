using Bogus;
using UserTransactions.Communication.Dtos.User.Response;

namespace UserTransactions.Tests.Shared.Builders.Dtos.Response.User
{
    public static class ResponseListTotalQuantityUserDtoBuilder
    {
        public static ResponseListTotalQuantityUserDto Build()
        {
            return new Faker<ResponseListTotalQuantityUserDto>()
                .RuleFor(x => x.TotalQuantity, f => f.Random.Int(1, 100))
                .Generate();
        }
    }
}