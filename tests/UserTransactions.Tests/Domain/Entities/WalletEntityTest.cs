using FluentAssertions;
using UserTransactions.Tests.Shared.Builders.Entities;
using WalletEntity = UserTransactions.Domain.Entities.Wallet;

namespace UserTransactions.Tests.Domain.Entities
{
    public class WalletEntityTest
    {
        [Fact]
        public void Given_ValidWallet_When_CalledConstructor_ShouldCreateWallet()
        {
            // Arrange
            var wallet = WalletEntityBuilder.Build();

            // Act
            var walletEntity = new WalletEntity(wallet.UserId, wallet.Balance);

            // Assert
            walletEntity.Should().NotBeNull();
            walletEntity.Id.Should().NotBeEmpty();
            walletEntity.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMinutes(1));
            walletEntity.UpdatedAt.Should().HaveHour(0);
            walletEntity.IsActive.Should().BeTrue();
            walletEntity.UserId.Should().Be(wallet.UserId);
            walletEntity.Balance.Should().Be(wallet.Balance);
        }

        [Fact]
        public void Given_Wallet_When_Activate_ShouldSetIsActiveToTrueAndUpdateUpdatedAt()
        {
            // Arrange
            var wallet = WalletEntityBuilder.Build();
            var walletEntity = new WalletEntity(wallet.UserId, wallet.Balance);

            // Act
            walletEntity.Activate();

            // Assert
            walletEntity.IsActive.Should().BeTrue();
            walletEntity.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMinutes(1));
        }

        [Fact]
        public void Given_Wallet_When_DeActivate_ShouldSetIsActiveToFalseAndUpdateUpdatedAt()
        {
            // Arrange
            var wallet = WalletEntityBuilder.Build();
            var walletEntity = new WalletEntity(wallet.UserId, wallet.Balance);

            // Act
            walletEntity.Deactivate();

            // Assert
            walletEntity.IsActive.Should().BeFalse();
            walletEntity.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMinutes(1));
        }
    }
}
