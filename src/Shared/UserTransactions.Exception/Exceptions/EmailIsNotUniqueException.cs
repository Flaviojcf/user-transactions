using System.Diagnostics.CodeAnalysis;

namespace UserTransactions.Exception.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class EmailIsNotUniqueException : UserTransactionsException
    {
        public EmailIsNotUniqueException() : base(ResourceMessagesException.EmailIsNotUnique) { }
    }
}
