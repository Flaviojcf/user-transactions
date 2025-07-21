using Bogus;
using UserTransactions.Communication.Dtos.Transaction.Response;

namespace UserTransactions.Tests.Shared.Builders.Dtos.Response.Transaction
{
    public static class ResponseListTransactionsDtoBuilder
    {
        public static ResponseListTransactionsDto Build()
        {
            return new Faker<ResponseListTransactionsDto>()
                .RuleFor(x => x.SenderName, f => f.Name.FullName())
                .RuleFor(x => x.ReceiverName, f => f.Name.FullName())
                .RuleFor(x => x.Amount, f => f.Random.Decimal(1, 1000))
                .RuleFor(x => x.CreatedAt, f => f.Date.Recent())
                .Generate();
        }

        public static IList<ResponseListTransactionsDto> BuildList(int count = 3)
        {
            return new Faker<ResponseListTransactionsDto>()
                .RuleFor(x => x.SenderName, f => f.Name.FullName())
                .RuleFor(x => x.ReceiverName, f => f.Name.FullName())
                .RuleFor(x => x.Amount, f => f.Random.Decimal(1, 1000))
                .RuleFor(x => x.CreatedAt, f => f.Date.Recent())
                .Generate(count);
        }
    }
}