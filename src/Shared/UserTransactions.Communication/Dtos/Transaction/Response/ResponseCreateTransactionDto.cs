namespace UserTransactions.Communication.Dtos.Transaction.Response
{
    public class ResponseCreateTransactionDto
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
    }
}
