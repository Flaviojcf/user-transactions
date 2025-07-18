using Bogus;
using UserTransactions.Domain.Entities;

namespace UserTransactions.Tests.Shared.Builders.Entities
{
    public static class WalletEntityBuilder
    {
        public static Wallet Build()
        {
            return new Faker<Wallet>().CustomInstantiator(faker => new Wallet(Guid.NewGuid())).Generate();
        }
    }
}
