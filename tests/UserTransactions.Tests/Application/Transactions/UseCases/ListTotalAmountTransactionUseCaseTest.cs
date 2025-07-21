using FluentAssertions;
using UserTransactions.Application.UseCases.Transaction.ListTotalAmount;
using UserTransactions.Domain.Repositories.Transaction;
using UserTransactions.Tests.Shared.Builders.Repositories;

namespace UserTransactions.Tests.Application.Transactions.UseCases
{
    public class ListTotalAmountTransactionUseCaseTest
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IListTotalAmountTransactionUseCase _sut;

        public ListTotalAmountTransactionUseCaseTest()
        {
            _transactionRepository = TransactionRepositoryBuilder.Build();
            _sut = new ListTotalAmountTransactionUseCase(_transactionRepository);
        }

        [Fact]
        public async Task Given_ValidRequest_When_ExecuteAsyncIsCalled_Then_ShouldReturnValidResult()
        {
            // Arrange
            var expectedTotalAmount = 5000.50m;
            TransactionRepositoryBuilder.SetupListTotalAmountAsync(expectedTotalAmount);

            // Act
            var result = await _sut.ExecuteAsync();

            // Assert
            result.Should().NotBeNull();
            result.TotalAmount.Should().Be(expectedTotalAmount);
        }

        [Fact]
        public async Task Given_ZeroTransactions_When_ExecuteAsyncIsCalled_Then_ShouldReturnZeroAmount()
        {
            // Arrange
            var expectedTotalAmount = 0m;
            TransactionRepositoryBuilder.SetupListTotalAmountAsync(expectedTotalAmount);

            // Act
            var result = await _sut.ExecuteAsync();

            // Assert
            result.Should().NotBeNull();
            result.TotalAmount.Should().Be(0);
        }

        [Theory]
        [InlineData(100.50)]
        [InlineData(1000.75)]
        [InlineData(10000.25)]
        public async Task Given_SpecificTotalAmount_When_ExecuteAsyncIsCalled_Then_ShouldReturnCorrectAmount(decimal totalAmount)
        {
            // Arrange
            TransactionRepositoryBuilder.SetupListTotalAmountAsync(totalAmount);

            // Act
            var result = await _sut.ExecuteAsync();

            // Assert
            result.Should().NotBeNull();
            result.TotalAmount.Should().Be(totalAmount);
        }
    }
}