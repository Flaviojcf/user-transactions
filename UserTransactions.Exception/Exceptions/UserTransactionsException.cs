using System.Diagnostics.CodeAnalysis;

namespace UserTransactions.Exception.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class UserTransactionsException : SystemException
    {
        public UserTransactionsException(string message) : base(message) { }
    }
}
