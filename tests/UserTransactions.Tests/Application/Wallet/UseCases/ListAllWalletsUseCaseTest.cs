using FluentAssertions;
using UserTransactions.Application.UseCases.Wallet.ListAll;
using UserTransactions.Domain.Enum;
using UserTransactions.Domain.Repositories.Wallet;
using UserTransactions.Tests.Shared.Builders.Entities;
using UserTransactions.Tests.Shared.Builders.Repositories;
using WalletEntity = UserTransactions.Domain.Entities.Wallet;

namespace UserTransactions.Tests.Application.Wallet.UseCases
{
    public class ListAllWalletsUseCaseTest
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IListAllWalletsUseCase _sut;

        public ListAllWalletsUseCaseTest()
        {
            _walletRepository = WalletRepositoryBuilder.Build();
            _sut = new ListAllWalletsUseCase(_walletRepository);
        }

        [Fact]
        public async Task Given_ValidRequest_When_ExecuteAsyncIsCalled_Then_ShouldReturnValidResult()
        {
            // Arrange
            var wallets = new List<WalletEntity>();
            for (int i = 0; i < 3; i++)
            {
                var user = UserEntityBuilder.BuildUser();
                var wallet = WalletEntityBuilder.BuildWithUser(user);
                wallets.Add(wallet);
            }
            WalletRepositoryBuilder.SetupListAllAsync(wallets);

            // Act
            var result = await _sut.ExecuteAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(3);
            result.Should().AllSatisfy(wallet =>
            {
                wallet.FullName.Should().NotBeNullOrEmpty();
                wallet.Email.Should().NotBeNullOrEmpty();
                wallet.Balance.Should().BeGreaterThanOrEqualTo(0);
                wallet.UserType.Should().BeDefined();
            });
        }

        [Fact]
        public async Task Given_NoWallets_When_ExecuteAsyncIsCalled_Then_ShouldReturnEmptyList()
        {
            // Arrange
            var wallets = new List<WalletEntity>();
            WalletRepositoryBuilder.SetupListAllAsync(wallets);

            // Act
            var result = await _sut.ExecuteAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(10)]
        public async Task Given_SpecificNumberOfWallets_When_ExecuteAsyncIsCalled_Then_ShouldReturnCorrectCount(int walletCount)
        {
            // Arrange
            var wallets = new List<WalletEntity>();
            for (int i = 0; i < walletCount; i++)
            {
                var user = UserEntityBuilder.BuildUser();
                var wallet = WalletEntityBuilder.BuildWithUser(user);
                wallets.Add(wallet);
            }
            WalletRepositoryBuilder.SetupListAllAsync(wallets);

            // Act
            var result = await _sut.ExecuteAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(walletCount);
        }

        [Fact]
        public async Task Given_WalletsWithDifferentUserTypes_When_ExecuteAsyncIsCalled_Then_ShouldReturnAllWallets()
        {
            // Arrange
            var wallets = new List<WalletEntity>();

            var userWallet = WalletEntityBuilder.BuildWithUser(UserEntityBuilder.BuildUser());
            var merchantWallet = WalletEntityBuilder.BuildWithUser(UserEntityBuilder.BuildMerchant());

            wallets.Add(userWallet);
            wallets.Add(merchantWallet);

            WalletRepositoryBuilder.SetupListAllAsync(wallets);

            // Act
            var result = await _sut.ExecuteAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().Contain(w => w.UserType == UserType.User);
            result.Should().Contain(w => w.UserType == UserType.Merchant);
        }
    }
}