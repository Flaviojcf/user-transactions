using System.Diagnostics.CodeAnalysis;

namespace UserTransactions.Communication.Dtos.Health.Response
{
    [ExcludeFromCodeCoverage]
    public class ResponseOverallHealthDto
    {
        public IList<ResponseHealthServiceDto> Services { get; set; }
    }
}