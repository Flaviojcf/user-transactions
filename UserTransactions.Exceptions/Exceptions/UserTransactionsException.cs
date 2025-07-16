using System.Diagnostics.CodeAnalysis;

namespace UserTransactions.Exceptions.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class UserTransactionsException : SystemException
    {
        public UserTransactionsException(string message) : base(message) { }
    }
}
