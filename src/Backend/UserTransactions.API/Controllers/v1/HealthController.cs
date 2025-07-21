using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using UserTransactions.Application.UseCases.Health.GetOverallHealth;
using UserTransactions.Communication.Dtos.Health.Response;

namespace UserTransactions.API.Controllers.v1
{
    [ExcludeFromCodeCoverage]
    [ApiVersion("1.0")]
    [Route("/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(ResponseOverallHealthDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOverallHealth([FromServices] IGetOverallHealthUseCase useCase)
        {
            var result = await useCase.ExecuteAsync();
            return Ok(result);
        }
    }
}
