using Moq;
using UserTransactions.Domain.Entities;
using UserTransactions.Domain.Repositories.Transaction;

namespace UserTransactions.Tests.Shared.Builders.Repositories
{
    public static class TransactionRepositoryBuilder
    {
        private static readonly Mock<ITransactionRepository> _mock = new Mock<ITransactionRepository>();

        public static ITransactionRepository Build() => _mock.Object;

        public static void SetupAddAsync()
        {
            _mock.Setup(x => x.AddAsync(It.IsAny<Transaction>())).Returns(Task.CompletedTask);
        }

        public static void SetupListAllAsync(IList<Transaction> transactions)
        {
            _mock.Setup(x => x.ListAllAsync()).ReturnsAsync(transactions);
        }

        public static void SetupListTotalAsync(int totalQuantity)
        {
            _mock.Setup(x => x.ListTotalAsync()).ReturnsAsync(totalQuantity);
        }

        public static void SetupListTotalAmountAsync(decimal totalAmount)
        {
            _mock.Setup(x => x.ListTotalAmountAsync()).ReturnsAsync(totalAmount);
        }

        public static void SetupListLatestFourAsync(IList<Transaction> transactions)
        {
            _mock.Setup(x => x.ListLatestFourAsync()).ReturnsAsync(transactions);
        }
    }
}
