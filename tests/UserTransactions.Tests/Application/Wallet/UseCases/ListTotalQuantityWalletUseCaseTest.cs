using FluentAssertions;
using UserTransactions.Application.UseCases.Wallet.ListTotal;
using UserTransactions.Domain.Repositories.Wallet;
using UserTransactions.Tests.Shared.Builders.Repositories;

namespace UserTransactions.Tests.Application.Wallet.UseCases
{
    public class ListTotalQuantityWalletUseCaseTest
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IListTotalQuantityWalletUseCase _sut;

        public ListTotalQuantityWalletUseCaseTest()
        {
            _walletRepository = WalletRepositoryBuilder.Build();
            _sut = new ListTotalQuantityWalletUseCase(_walletRepository);
        }

        [Fact]
        public async Task Given_ValidRequest_When_ExecuteAsyncIsCalled_Then_ShouldReturnValidResult()
        {
            // Arrange
            var expectedTotalQuantity = 35;
            WalletRepositoryBuilder.SetupListTotalQuantityAsync(expectedTotalQuantity);

            // Act
            var result = await _sut.ExecuteAsync();

            // Assert
            result.Should().NotBeNull();
            result.TotalQuantity.Should().Be(expectedTotalQuantity);
        }

        [Fact]
        public async Task Given_ZeroWallets_When_ExecuteAsyncIsCalled_Then_ShouldReturnZeroQuantity()
        {
            // Arrange
            var expectedTotalQuantity = 0;
            WalletRepositoryBuilder.SetupListTotalQuantityAsync(expectedTotalQuantity);

            // Act
            var result = await _sut.ExecuteAsync();

            // Assert
            result.Should().NotBeNull();
            result.TotalQuantity.Should().Be(0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(15)]
        [InlineData(75)]
        [InlineData(150)]
        public async Task Given_SpecificTotalQuantity_When_ExecuteAsyncIsCalled_Then_ShouldReturnCorrectQuantity(int totalQuantity)
        {
            // Arrange
            WalletRepositoryBuilder.SetupListTotalQuantityAsync(totalQuantity);

            // Act
            var result = await _sut.ExecuteAsync();

            // Assert
            result.Should().NotBeNull();
            result.TotalQuantity.Should().Be(totalQuantity);
        }

        [Fact]
        public async Task Given_LargeNumberOfWallets_When_ExecuteAsyncIsCalled_Then_ShouldReturnCorrectQuantity()
        {
            // Arrange
            var expectedTotalQuantity = 500;
            WalletRepositoryBuilder.SetupListTotalQuantityAsync(expectedTotalQuantity);

            // Act
            var result = await _sut.ExecuteAsync();

            // Assert
            result.Should().NotBeNull();
            result.TotalQuantity.Should().Be(expectedTotalQuantity);
        }
    }
}