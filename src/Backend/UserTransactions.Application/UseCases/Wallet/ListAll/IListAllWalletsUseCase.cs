using UserTransactions.Communication.Dtos.Wallet.Response;

namespace UserTransactions.Application.UseCases.Wallet.ListAll
{
    public interface IListAllWalletsUseCase
    {
        Task<IList<ResponseListAllWalletsDto>> ExecuteAsync();
    }
}
