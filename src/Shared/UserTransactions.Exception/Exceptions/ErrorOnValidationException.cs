using System.Diagnostics.CodeAnalysis;

namespace UserTransactions.Exception.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class ErrorOnValidationException : UserTransactionsException
    {
        public ErrorOnValidationException(IList<string> errors) : base(string.Empty)
        {
            Errors = errors;
        }

        public IList<string> Errors { get; set; }
    }
}
