using UserTransactions.Communication.Dtos.User.Request;
using UserTransactions.Communication.Dtos.User.Response;

namespace UserTransactions.Application.UseCases.User.Create
{
    public interface ICreateUserUseCase
    {
        Task<ResponseCreateUserDto> ExecuteAsync(RequestCreateUserDto requestCreateUserDto);
    }
}
