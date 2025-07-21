using UserTransactions.Communication.Dtos.Wallet.Response;

namespace UserTransactions.Application.UseCases.Wallet.ListTotal
{
    public interface IListTotalQuantityWalletUseCase
    {
        Task<ResponseListTotalQuantityWalletDto> ExecuteAsync();

    }
}
