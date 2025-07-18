namespace UserTransactions.Communication.Dtos.Transaction.Request
{
    public class RequestCreateTransactionDto
    {
        public decimal Amount { get; set; }
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
    }
}
