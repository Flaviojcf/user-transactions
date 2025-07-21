using FluentAssertions;
using UserTransactions.Application.UseCases.Transaction.ListLatestFourTransactions;
using UserTransactions.Domain.Entities;
using UserTransactions.Domain.Repositories.Transaction;
using UserTransactions.Tests.Shared.Builders.Entities;
using UserTransactions.Tests.Shared.Builders.Repositories;

namespace UserTransactions.Tests.Application.Transactions.UseCases
{
    public class ListLatestFourTransactionUseCaseTest
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IListListLatestFourTransactionsUseCase _sut;

        public ListLatestFourTransactionUseCaseTest()
        {
            _transactionRepository = TransactionRepositoryBuilder.Build();
            _sut = new ListLatestFourTransactionsUseCase(_transactionRepository);
        }

        [Fact]
        public async Task Given_ValidRequest_When_ExecuteAsyncIsCalled_Then_ShouldReturnValidResult()
        {
            // Arrange
            var transactions = TransactionEntityBuilder.BuildListWithNavigationProperties(4);
            TransactionRepositoryBuilder.SetupListLatestFourAsync(transactions);

            // Act
            var result = await _sut.ExecuteAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(4);
            result.Should().AllSatisfy(transaction =>
            {
                transaction.SenderName.Should().NotBeNullOrEmpty();
                transaction.ReceiverName.Should().NotBeNullOrEmpty();
                transaction.Amount.Should().BeGreaterThan(0);
                transaction.CreatedAt.Should().NotBe(default);
            });
        }

        [Fact]
        public async Task Given_LessThanFourTransactions_When_ExecuteAsyncIsCalled_Then_ShouldReturnExistingTransactions()
        {
            // Arrange
            var transactions = TransactionEntityBuilder.BuildListWithNavigationProperties(2);
            TransactionRepositoryBuilder.SetupListLatestFourAsync(transactions);

            // Act
            var result = await _sut.ExecuteAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().AllSatisfy(transaction =>
            {
                transaction.SenderName.Should().NotBeNullOrEmpty();
                transaction.ReceiverName.Should().NotBeNullOrEmpty();
                transaction.Amount.Should().BeGreaterThan(0);
                transaction.CreatedAt.Should().NotBe(default);
            });
        }

        [Fact]
        public async Task Given_NoTransactions_When_ExecuteAsyncIsCalled_Then_ShouldReturnEmptyList()
        {
            // Arrange
            var transactions = new List<Transaction>();
            TransactionRepositoryBuilder.SetupListLatestFourAsync(transactions);

            // Act
            var result = await _sut.ExecuteAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }
    }
}