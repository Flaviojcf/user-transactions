using System.Diagnostics.CodeAnalysis;
using System.Net;
using UserTransactions.Exception;

namespace UserTransactions.Communication.Dtos.Errors.Response
{
    [ExcludeFromCodeCoverage]
    public class ResponseErrorDto
    {
        public ResponseErrorDto(IList<string> messages, HttpStatusCode statusCode)
        {
            TraceId = Guid.NewGuid();
            Type = string.Format(ResourceMessagesException.TypeError, (int)statusCode);
            StatusCode = statusCode;
            ErrorDetails = new ErrorDetails(messages);
        }

        public ResponseErrorDto(string error, HttpStatusCode statusCode)
        {
            TraceId = Guid.NewGuid();
            Type = string.Format(ResourceMessagesException.TypeError, (int)statusCode);
            StatusCode = statusCode;
            ErrorDetails = new ErrorDetails([error]);
        }

        public Guid TraceId { get; private set; }
        public string Type { get; private set; }
        public HttpStatusCode StatusCode { get; set; }
        public ErrorDetails ErrorDetails { get; private set; }
    }
}
