using Bogus;
using UserTransactions.Domain.Entities;

namespace UserTransactions.Tests.Shared.Builders.Entities
{
    public static class WalletEntityBuilder
    {
        public static Wallet Build()
        {
            var wallet = new Faker<Wallet>().CustomInstantiator(faker => new Wallet(Guid.NewGuid())).Generate();
            var user = UserEntityBuilder.BuildUser();
            wallet.SetUser(user);
            return wallet;
        }

        public static Wallet BuildWithUser(User user)
        {
            var wallet = new Faker<Wallet>().CustomInstantiator(faker => new Wallet(user.Id)).Generate();
            wallet.SetUser(user);
            return wallet;
        }
    }
}
