using FluentAssertions;
using UserTransactions.Exception;
using UserTransactions.Exception.Exceptions;
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

        [Fact]
        public void Given_Wallet_When_DebitWithInsufficientBalance_ShouldThrowDomainException()
        {
            // Arrange
            var wallet = WalletEntityBuilder.Build();
            var walletEntity = new WalletEntity(wallet.UserId, 0);

            // Act
            Action action = () => walletEntity.Debit(100);

            // Assert
            action.Should().Throw<DomainException>()
                .WithMessage(ResourceMessagesException.InsufficientBalance);
        }

        [Fact]
        public void Given_Wallet_When_Credit_ShouldIncreaseBalance()
        {
            // Arrange
            var wallet = WalletEntityBuilder.Build();
            var walletEntity = new WalletEntity(wallet.UserId, wallet.Balance);

            // Act
            walletEntity.Credit(100);

            // Assert
            walletEntity.Balance.Should().Be(wallet.Balance + 100);
        }
    }
}
