using System.Diagnostics.CodeAnalysis;

namespace UserTransactions.Communication.Dtos.Health.Response
{
    [ExcludeFromCodeCoverage]
    public class ResponseHealthServiceDto
    {
        public string Service { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
