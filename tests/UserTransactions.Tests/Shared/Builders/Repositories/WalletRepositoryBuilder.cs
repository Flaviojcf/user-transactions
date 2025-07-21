using Moq;
using UserTransactions.Domain.Repositories.Wallet;
using WalletEntity = UserTransactions.Domain.Entities.Wallet;

namespace UserTransactions.Tests.Shared.Builders.Repositories
{
    public static class WalletRepositoryBuilder
    {
        private static readonly Mock<IWalletRepository> _mock = new Mock<IWalletRepository>();

        public static IWalletRepository Build() => _mock.Object;

        public static void SetupAddAsync()
        {
            _mock.Setup(x => x.AddAsync(It.IsAny<WalletEntity>())).Returns(Task.CompletedTask);
        }

        public static void SetupExistsByUserIdAsync(Guid userId, bool exists)
        {
            _mock.Setup(x => x.ExistsByUserIdAsync(userId)).ReturnsAsync(exists);
        }

        public static void VerifyAddAsyncWasCalled(WalletEntity wallet)
        {
            _mock.Verify(x => x.AddAsync(wallet));
        }

        public static void VerifyAddAsyncWasCalled()
        {
            _mock.Verify(x => x.AddAsync(It.IsAny<WalletEntity>()));
        }

        public static void SetupExistsByIdAsync(Guid id, bool exists)
        {
            _mock.Setup(x => x.ExistsByIdAsync(id)).ReturnsAsync(exists);
        }

        public static void SetupGetByIdAsync(Guid id, WalletEntity? wallet)
        {
            _mock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(wallet);
        }

        public static void SetupUpdateAsync()
        {
            _mock.Setup(x => x.UpdateAsync(It.IsAny<WalletEntity>())).Returns(Task.CompletedTask);
        }

        public static void SetupListTotalQuantityAsync(int totalQuantity)
        {
            _mock.Setup(x => x.ListTotalQuantityAsync()).ReturnsAsync(totalQuantity);
        }
    }
}
