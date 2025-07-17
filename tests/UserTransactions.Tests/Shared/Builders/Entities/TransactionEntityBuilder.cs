using Bogus;
using UserTransactions.Domain.Entities;

namespace UserTransactions.Tests.Shared.Builders.Entities
{
    public static class TransactionEntityBuilder
    {
        public static Transaction Build()
        {
            return new Faker<Transaction>().CustomInstantiator(faker => new Transaction(Guid.NewGuid(), Guid.NewGuid(), faker.Random.Decimal(1, 1000))).Generate();
        }
    }
}
