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
            var walletEntity = new WalletEntity(wallet.UserId);

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
            var walletEntity = new WalletEntity(wallet.UserId);

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
            var walletEntity = new WalletEntity(wallet.UserId);

            // Act
            walletEntity.Deactivate();

            // Assert
            walletEntity.IsActive.Should().BeFalse();
            walletEntity.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMinutes(1));
        }

        [Theory]
        [InlineData(600)]
        [InlineData(700)]
        [InlineData(800)]
        [InlineData(501)]
        [InlineData(502)]
        public void Given_Wallet_When_DebitWithInsufficientBalance_ShouldThrowDomainException(decimal amount)
        {
            // Arrange
            var wallet = WalletEntityBuilder.Build();
            var user = UserEntityBuilder.BuildUser();
            var walletEntity = new WalletEntity(wallet.UserId);
            walletEntity.SetUser(user);

            // Act
            Action action = () => walletEntity.Debit(amount);

            // Assert
            action.Should().Throw<DomainException>()
                .WithMessage(ResourceMessagesException.InsufficientBalance);
        }

        [Fact]
        public void Given_Wallet_When_UserTypeIsMerchant_ShouldThrowDomainException()
        {
            // Arrange
            var wallet = WalletEntityBuilder.Build();
            var user = UserEntityBuilder.BuildMerchant();
            var walletEntity = new WalletEntity(wallet.UserId);
            walletEntity.SetUser(user);

            // Act
            Action action = () => walletEntity.Debit(10);

            // Assert
            action.Should().Throw<DomainException>()
                .WithMessage(ResourceMessagesException.MerchantCannotDebit);
        }

        [Fact]
        public void Given_Wallet_When_Credit_ShouldIncreaseBalance()
        {
            // Arrange
            var wallet = WalletEntityBuilder.Build();
            var walletEntity = new WalletEntity(wallet.UserId);

            // Act
            walletEntity.Credit(100);

            // Assert
            walletEntity.Balance.Should().Be(wallet.Balance + 100);
        }
    }
}
