using Bogus;
using UserTransactions.Communication.Dtos.Transaction.Response;

namespace UserTransactions.Tests.Shared.Builders.Dtos.Response.Transaction
{
    public static class ResponseTotalAmountTransactionsDtoBuilder
    {
        public static ResponseTotalAmountTransactionsDto Build()
        {
            return new Faker<ResponseTotalAmountTransactionsDto>()
                .RuleFor(x => x.TotalAmount, f => f.Random.Decimal(100, 10000))
                .Generate();
        }
    }
}