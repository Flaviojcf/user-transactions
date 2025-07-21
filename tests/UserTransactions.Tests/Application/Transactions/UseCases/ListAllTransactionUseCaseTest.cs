using FluentAssertions;
using UserTransactions.Application.UseCases.Transaction.ListAll;
using UserTransactions.Domain.Entities;
using UserTransactions.Domain.Repositories.Transaction;
using UserTransactions.Tests.Shared.Builders.Entities;
using UserTransactions.Tests.Shared.Builders.Repositories;

namespace UserTransactions.Tests.Application.Transactions.UseCases
{
    public class ListAllTransactionUseCaseTest
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IListAllTransactionUseCase _sut;

        public ListAllTransactionUseCaseTest()
        {
            _transactionRepository = TransactionRepositoryBuilder.Build();
            _sut = new ListAllTransactionUseCase(_transactionRepository);
        }

        [Fact]
        public async Task Given_ValidRequest_When_ExecuteAsyncIsCalled_Then_ShouldReturnValidResult()
        {
            // Arrange
            var transactions = TransactionEntityBuilder.BuildListWithNavigationProperties(3);
            TransactionRepositoryBuilder.SetupListAllAsync(transactions);

            // Act
            var result = await _sut.ExecuteAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(3);
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
            TransactionRepositoryBuilder.SetupListAllAsync(transactions);

            // Act
            var result = await _sut.ExecuteAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }
    }
}