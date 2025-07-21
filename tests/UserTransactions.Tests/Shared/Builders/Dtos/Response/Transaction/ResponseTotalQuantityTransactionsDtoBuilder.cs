using Bogus;
using UserTransactions.Communication.Dtos.Transaction.Response;

namespace UserTransactions.Tests.Shared.Builders.Dtos.Response.Transaction
{
    public static class ResponseTotalQuantityTransactionsDtoBuilder
    {
        public static ResponseTotalQuantityTransactionsDto Build()
        {
            return new Faker<ResponseTotalQuantityTransactionsDto>()
                .RuleFor(x => x.TotalQuantity, f => f.Random.Int(1, 100))
                .Generate();
        }
    }
}