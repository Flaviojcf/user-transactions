namespace UserTransactions.Domain.Exceptions
{
    public class DomainException : UserTransactionsException
    {
        public DomainException(string message) : base(message) { }
    }
}
