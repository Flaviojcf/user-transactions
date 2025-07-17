using System.Diagnostics.CodeAnalysis;

namespace UserTransactions.Exception.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class DomainException : UserTransactionsException
    {
        public DomainException(string message) : base(message) { }
    }
}
