using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using UserTransactions.Application.UseCases.Transaction.Create;
using UserTransactions.Communication.Dtos.Errors.Response;
using UserTransactions.Communication.Dtos.Transaction.Request;
using UserTransactions.Communication.Dtos.Transaction.Response;

namespace UserTransactions.API.Controllers.v1
{
    //TODO: Criar teste de integração para o controller
    [ExcludeFromCodeCoverage]
    [ApiVersion("1.0")]
    [Route("/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        //TODO: Pesquisar como alterar o exemplo de retorno do swagger
        [HttpPost("register")]
        [ProducesResponseType(typeof(ResponseCreateTransactionDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterTransaction([FromBody] RequestCreateTransactionDto request, [FromServices] ICreateTransactionUseCase useCase)
        {
            var result = await useCase.ExecuteAsync(request);

            return CreatedAtAction(nameof(RegisterTransaction), new { id = result.Id }, result);
        }
    }
}
