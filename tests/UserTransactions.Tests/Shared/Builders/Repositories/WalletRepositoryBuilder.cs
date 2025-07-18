using Moq;
using UserTransactions.Domain.Repositories.Wallet;

namespace UserTransactions.Tests.Shared.Builders.Repositories
{
    public static class WalletRepositoryBuilder
    {
        private static readonly Mock<IWalletRepository> _mock = new Mock<IWalletRepository>();
        public static IWalletRepository Build() => _mock.Object;
    }
}
