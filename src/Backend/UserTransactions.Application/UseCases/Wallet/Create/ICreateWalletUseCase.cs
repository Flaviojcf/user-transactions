using UserTransactions.Communication.Dtos.Wallet.Request;
using UserTransactions.Communication.Dtos.Wallet.Response;

namespace UserTransactions.Application.UseCases.Wallet.Create
{
    public interface ICreateWalletUseCase
    {
        Task<ResponseCreateWalletDto> ExecuteAsync(RequestCreateWalletDto requestCreateWalletDto);
    }
}
