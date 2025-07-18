using System.Diagnostics.CodeAnalysis;

namespace UserTransactions.Communication.Dtos.Errors.Response
{
    [ExcludeFromCodeCoverage]
    public class ErrorDetails
    {
        public ErrorDetails(IList<string> messages)
        {
            Messages = messages;
        }

        public IList<string> Messages { get; set; }
    }
}
