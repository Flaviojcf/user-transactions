using System.Diagnostics.CodeAnalysis;

namespace UserTransactions.Consumer.Events
{
    [ExcludeFromCodeCoverage]
    public class TransactionCreatedEvent
    {
        public TransactionCreatedEvent(Guid transactionId, decimal amount, Guid senderId, Guid receiverId, string receiverEmail)
        {
            TransactionId = transactionId;
            Amount = amount;
            SenderId = senderId;
            ReceiverId = receiverId;
            CreatedAt = DateTime.UtcNow;
            ReceiverEmail = receiverEmail;
        }

        public Guid TransactionId { get; }
        public decimal Amount { get; }
        public Guid SenderId { get; }
        public Guid ReceiverId { get; }
        public DateTime CreatedAt { get; }
        public string ReceiverEmail { get; }
    }
}