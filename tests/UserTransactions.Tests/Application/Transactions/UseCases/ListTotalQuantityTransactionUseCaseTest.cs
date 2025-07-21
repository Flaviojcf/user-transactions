using FluentAssertions;
using UserTransactions.Application.UseCases.Transaction.ListTotal;
using UserTransactions.Domain.Repositories.Transaction;
using UserTransactions.Tests.Shared.Builders.Repositories;

namespace UserTransactions.Tests.Application.Transactions.UseCases
{
    public class ListTotalQuantityTransactionUseCaseTest
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IListTotalQuantityTransactionUseCase _sut;

        public ListTotalQuantityTransactionUseCaseTest()
        {
            _transactionRepository = TransactionRepositoryBuilder.Build();
            _sut = new ListTotalQuantityTransactionUseCase(_transactionRepository);
        }

        [Fact]
        public async Task Given_ValidRequest_When_ExecuteAsyncIsCalled_Then_ShouldReturnValidResult()
        {
            // Arrange
            var expectedTotalQuantity = 25;
            TransactionRepositoryBuilder.SetupListTotalAsync(expectedTotalQuantity);

            // Act
            var result = await _sut.ExecuteAsync();

            // Assert
            result.Should().NotBeNull();
            result.TotalQuantity.Should().Be(expectedTotalQuantity);
        }

        [Fact]
        public async Task Given_ZeroTransactions_When_ExecuteAsyncIsCalled_Then_ShouldReturnZeroQuantity()
        {
            // Arrange
            var expectedTotalQuantity = 0;
            TransactionRepositoryBuilder.SetupListTotalAsync(expectedTotalQuantity);

            // Act
            var result = await _sut.ExecuteAsync();

            // Assert
            result.Should().NotBeNull();
            result.TotalQuantity.Should().Be(0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(50)]
        [InlineData(100)]
        public async Task Given_SpecificTotalQuantity_When_ExecuteAsyncIsCalled_Then_ShouldReturnCorrectQuantity(int totalQuantity)
        {
            // Arrange
            TransactionRepositoryBuilder.SetupListTotalAsync(totalQuantity);

            // Act
            var result = await _sut.ExecuteAsync();

            // Assert
            result.Should().NotBeNull();
            result.TotalQuantity.Should().Be(totalQuantity);
        }
    }
}