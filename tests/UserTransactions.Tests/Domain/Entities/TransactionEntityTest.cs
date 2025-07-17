using FluentAssertions;
using UserTransactions.Domain.Entities;
using UserTransactions.Tests.Shared.Builders.Entities;

namespace UserTransactions.Tests.Domain.Entities
{
    public class TransactionEntityTest
    {
        [Fact]
        public void Given_ValidTransaction_When_CalledConstructor_ShouldCreateTransaction()
        {
            // Arrange
            var transaction = TransactionEntityBuilder.Build();

            // Act
            var transactionEntity = new Transaction(transaction.SenderId, transaction.ReceiverId, transaction.Amount);

            // Assert
            transactionEntity.Should().NotBeNull();

            transactionEntity.Id.Should().NotBeEmpty();
            transactionEntity.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMinutes(1));
            transactionEntity.UpdatedAt.Should().HaveHour(0);
            transactionEntity.IsActive.Should().BeTrue();

            transactionEntity.SenderId.Should().Be(transaction.SenderId);
            transactionEntity.ReceiverId.Should().Be(transaction.ReceiverId);
            transactionEntity.Amount.Should().Be(transaction.Amount);
        }
    }
}
