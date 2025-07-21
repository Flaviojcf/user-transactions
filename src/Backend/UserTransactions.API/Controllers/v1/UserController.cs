using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using UserTransactions.Application.UseCases.User.Create;
using UserTransactions.Application.UseCases.User.ListTotal;
using UserTransactions.Communication.Dtos.Errors.Response;
using UserTransactions.Communication.Dtos.User.Request;
using UserTransactions.Communication.Dtos.User.Response;

namespace UserTransactions.API.Controllers.v1
{
    //TODO: Criar teste de integração para o controller
    [ExcludeFromCodeCoverage]
    [ApiVersion("1.0")]
    [Route("/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //TODO: Pesquisar como alterar o exemplo de retorno do swagger
        [HttpPost("register")]
        [ProducesResponseType(typeof(ResponseCreateUserDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterUser([FromBody] RequestCreateUserDto request, [FromServices] ICreateUserUseCase useCase)
        {
            var result = await useCase.ExecuteAsync(request);

            return CreatedAtAction(nameof(RegisterUser), new { id = result.Id }, result);
        }

        [HttpGet("list-total-quantity")]
        [ProducesResponseType(typeof(ResponseListTotalQuantityUserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ListTotalQuantityUsers([FromServices] IListTotalQuantityUserUseCase useCase)
        {
            var result = await useCase.ExecuteAsync();
            return Ok(result);
        }

    }
}
