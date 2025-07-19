using Moq;
using UserTransactions.Domain.Repositories;

namespace UserTransactions.Tests.Shared.Builders.Repositories
{
    public static class UnitOfWorkRepositoryBuilder
    {
        private static readonly Mock<IUnitOfWorkRepository> _mock = new Mock<IUnitOfWorkRepository>();

        public static IUnitOfWorkRepository Build() => _mock.Object;

        public static void SetupTransactionMethods()
        {
            _mock.Setup(x => x.BeginTransactionAsync()).Returns(Task.CompletedTask);
            _mock.Setup(x => x.CommitAsync()).Returns(Task.CompletedTask);
            _mock.Setup(x => x.RollbackAsync()).Returns(Task.CompletedTask);
        }
    }
}
