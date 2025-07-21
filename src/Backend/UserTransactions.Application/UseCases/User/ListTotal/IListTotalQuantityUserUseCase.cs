using UserTransactions.Communication.Dtos.User.Response;

namespace UserTransactions.Application.UseCases.User.ListTotal
{
    public interface IListTotalQuantityUserUseCase
    {
        Task<ResponseListTotalQuantityUserDto> ExecuteAsync();
    }
}
