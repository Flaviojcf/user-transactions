using System.Diagnostics.CodeAnalysis;

namespace UserTransactions.Domain.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class UserTransactionsException : SystemException
    {
        public UserTransactionsException(string message) : base(message) { }
    }
}
