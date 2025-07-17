using System.Diagnostics.CodeAnalysis;

namespace UserTransactions.Exception.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class CpfIsNotUniqueException : UserTransactionsException
    {
        public CpfIsNotUniqueException() : base(ResourceMessagesException.CpfIsNotUnique) { }
    }
}
