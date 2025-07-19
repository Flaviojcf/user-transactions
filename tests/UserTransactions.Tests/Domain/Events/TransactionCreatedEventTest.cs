using FluentAssertions;
using UserTransactions.Domain.Events;

namespace UserTransactions.Tests.Domain.Events
{
    public class TransactionCreatedEventTest
    {
        [Fact]
        public void Given_ValidParameters_When_CreateTransactionCreatedEvent_Then_ShouldCreateEventWithCorrectValues()
        {
            // Arrange
            var transactionId = Guid.NewGuid();
            var amount = 100.50m;
            var senderId = Guid.NewGuid();
            var receiverId = Guid.NewGuid();
            var receiverEmail = "receiver@test.com";
            var beforeCreation = DateTime.UtcNow;

            // Act
            var eventObj = new TransactionCreatedEvent(transactionId, amount, senderId, receiverId, receiverEmail);
            var afterCreation = DateTime.UtcNow;

            // Assert
            eventObj.Should().NotBeNull();
            eventObj.TransactionId.Should().Be(transactionId);
            eventObj.Amount.Should().Be(amount);
            eventObj.SenderId.Should().Be(senderId);
            eventObj.ReceiverId.Should().Be(receiverId);
            eventObj.ReceiverEmail.Should().Be(receiverEmail);
            eventObj.CreatedAt.Should().BeOnOrAfter(beforeCreation);
            eventObj.CreatedAt.Should().BeOnOrBefore(afterCreation);
        }

        [Fact]
        public void Given_ZeroAmount_When_CreateTransactionCreatedEvent_Then_ShouldCreateEventWithZeroAmount()
        {
            // Arrange
            var transactionId = Guid.NewGuid();
            var amount = 0m;
            var senderId = Guid.NewGuid();
            var receiverId = Guid.NewGuid();
            var receiverEmail = "receiver@test.com";

            // Act
            var eventObj = new TransactionCreatedEvent(transactionId, amount, senderId, receiverId, receiverEmail);

            // Assert
            eventObj.Amount.Should().Be(0m);
        }

        [Fact]
        public void Given_EmptyGuids_When_CreateTransactionCreatedEvent_Then_ShouldCreateEventWithEmptyGuids()
        {
            // Arrange
            var transactionId = Guid.Empty;
            var amount = 100m;
            var senderId = Guid.Empty;
            var receiverId = Guid.Empty;
            var receiverEmail = "receiver@test.com";

            // Act
            var eventObj = new TransactionCreatedEvent(transactionId, amount, senderId, receiverId, receiverEmail);

            // Assert
            eventObj.TransactionId.Should().Be(Guid.Empty);
            eventObj.SenderId.Should().Be(Guid.Empty);
            eventObj.ReceiverId.Should().Be(Guid.Empty);
        }
    }
}