using UserTransactions.Communication.Dtos.User.Response;

namespace UserTransactions.Application.UseCases.User.ListAll
{
    public interface IListAllUsersUseCase
    {
        Task<IList<ResponseListAllUsersDto>> ExecuteAsync();
    }
}
