using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using UserTransactions.Application.UseCases.Transaction.Create;
using UserTransactions.Application.UseCases.Transaction.ListAll;
using UserTransactions.Application.UseCases.Transaction.ListLatestFourTransactions;
using UserTransactions.Application.UseCases.Transaction.ListTotal;
using UserTransactions.Application.UseCases.Transaction.ListTotalAmount;
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

        [HttpGet("list-all")]
        [ProducesResponseType(typeof(ResponseListTransactionsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ListAllTransactions([FromServices] IListAllTransactionUseCase useCase)
        {
            var result = await useCase.ExecuteAsync();
            return Ok(result);
        }

        [HttpGet("list-total-quantity")]
        [ProducesResponseType(typeof(ResponseTotalQuantityTransactionsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ListTotalQuantityTransactions([FromServices] IListTotalQuantityTransactionUseCase useCase)
        {
            var result = await useCase.ExecuteAsync();
            return Ok(result);
        }

        [HttpGet("list-total-amount")]
        [ProducesResponseType(typeof(ResponseTotalAmountTransactionsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ListTotalAmountTransactions([FromServices] IListTotalAmountTransactionUseCase useCase)
        {
            var result = await useCase.ExecuteAsync();
            return Ok(result);
        }

        [HttpGet("list-latest-four")]
        [ProducesResponseType(typeof(ResponseListTransactionsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ListLatestFourTransactions([FromServices] IListListLatestFourTransactionsUseCase useCase)
        {
            var result = await useCase.ExecuteAsync();
            return Ok(result);
        }
    }
}
