using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using UserTransactions.Application.UseCases.Wallet.Create;
using UserTransactions.Application.UseCases.Wallet.ListTotal;
using UserTransactions.Communication.Dtos.Errors.Response;
using UserTransactions.Communication.Dtos.User.Response;
using UserTransactions.Communication.Dtos.Wallet.Request;
using UserTransactions.Communication.Dtos.Wallet.Response;

namespace UserTransactions.API.Controllers.v1
{
    //TODO: Criar teste de integração para o controller
    [ExcludeFromCodeCoverage]
    [ApiVersion("1.0")]
    [Route("/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        //TODO: Pesquisar como alterar o exemplo de retorno do swagger
        [HttpPost("register")]
        [ProducesResponseType(typeof(ResponseCreateWalletDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterWallet([FromBody] RequestCreateWalletDto request, [FromServices] ICreateWalletUseCase useCase)
        {
            var result = await useCase.ExecuteAsync(request);

            return CreatedAtAction(nameof(RegisterWallet), new { id = result.Id }, result);
        }

        [HttpGet("list-total-quantity")]
        [ProducesResponseType(typeof(ResponseListTotalQuantityWalletDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ListTotalQuantityWallets([FromServices] IListTotalQuantityWalletUseCase useCase)
        {
            var result = await useCase.ExecuteAsync();
            return Ok(result);
        }
    }
}