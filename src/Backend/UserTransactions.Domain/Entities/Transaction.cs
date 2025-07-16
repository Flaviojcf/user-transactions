namespace UserTransactions.Domain.Entities
{
    public class Transaction : BaseEntity
    {
        public Transaction(Guid senderId, Guid receiverId, decimal amount)
        {
            SenderId = senderId;
            ReceiverId = receiverId;
            Amount = amount;
        }

        public decimal Amount { get; private set; }

        public Guid SenderId { get; private set; }
        public Wallet? Sender { get; private set; }

        public Guid ReceiverId { get; private set; }
        public Wallet? Receiver { get; private set; }
    }
}
