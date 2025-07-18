using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using UserTransactions.Application.UseCases.User.Create;
using UserTransactions.Communication.Dtos.Errors.Response;
using UserTransactions.Communication.Dtos.User.Request;
using UserTransactions.Communication.Dtos.User.Response;

namespace UserTransactions.API.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //TODO: Pesquisar como alterar o exemplo de retorno do swagger
        [HttpPost("register")]
        [ProducesResponseType(typeof(ResponseCreateUserDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorDto), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterUser([FromBody] RequestCreateUserDto request, [FromServices] ICreateUserUseCase useCase)
        {
            var result = await useCase.ExecuteAsync(request);

            return CreatedAtAction(nameof(RegisterUser), new { id = result.Id }, result);
        }

    }
}
