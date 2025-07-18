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
    }
}
