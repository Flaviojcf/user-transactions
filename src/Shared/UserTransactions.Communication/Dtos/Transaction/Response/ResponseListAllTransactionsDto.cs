namespace UserTransactions.Communication.Dtos.Transaction.Response
{
    public class ResponseListTransactionsDto
    {
        public Guid Id { get; set; }
        public string SenderName { get; set; } = string.Empty;
        public string ReceiverName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
