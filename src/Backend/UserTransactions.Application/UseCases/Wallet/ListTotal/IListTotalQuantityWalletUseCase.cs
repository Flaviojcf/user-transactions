using UserTransactions.Communication.Dtos.User.Response;

namespace UserTransactions.Application.UseCases.Wallet.ListTotal
{
    public interface IListTotalQuantityWalletUseCase
    {
        Task<ResponseListTotalQuantityWalletDto> ExecuteAsync();

    }
}
