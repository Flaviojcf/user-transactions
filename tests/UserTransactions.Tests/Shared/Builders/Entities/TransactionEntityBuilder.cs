using Bogus;
using System.Reflection;
using UserTransactions.Domain.Entities;

namespace UserTransactions.Tests.Shared.Builders.Entities
{
    public static class TransactionEntityBuilder
    {
        public static Transaction Build()
        {
            return new Faker<Transaction>().CustomInstantiator(faker => new Transaction(Guid.NewGuid(), Guid.NewGuid(), faker.Random.Decimal(1, 1000))).Generate();
        }

        public static IList<Transaction> BuildListWithNavigationProperties(int count = 3)
        {
            var transactions = new List<Transaction>();
            var faker = new Faker();

            for (int i = 0; i < count; i++)
            {
                var senderWallet = WalletEntityBuilder.Build();
                var receiverWallet = WalletEntityBuilder.Build();
                var senderUser = UserEntityBuilder.BuildUser();
                var receiverUser = UserEntityBuilder.BuildUser();

                senderWallet.SetUser(senderUser);
                receiverWallet.SetUser(receiverUser);

                var transaction = new Transaction(senderWallet.Id, receiverWallet.Id, faker.Random.Decimal(1, 1000));

                SetNavigationProperty(transaction, "Sender", senderWallet);
                SetNavigationProperty(transaction, "Receiver", receiverWallet);

                transactions.Add(transaction);
            }

            return transactions;
        }

        private static void SetNavigationProperty(Transaction transaction, string propertyName, Wallet wallet)
        {
            var property = typeof(Transaction).GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            property?.SetValue(transaction, wallet);
        }
    }
}
