using System.Diagnostics.CodeAnalysis;

namespace UserTransactions.Exception.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class ErrorOnAuthorizeException : UserTransactionsException
    {
        public ErrorOnAuthorizeException(IList<string> errors) : base(string.Empty)
        {
            Errors = errors;
        }

        public IList<string> Errors { get; set; }
    }
}
