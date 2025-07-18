using UserTransactions.Communication.Dtos.Wallet.Request;
using UserTransactions.Communication.Dtos.Wallet.Response;
using UserTransactions.Domain.Repositories.Wallet;

namespace UserTransactions.Application.UseCases.Wallet.Create
{
    public class CreateWalletUseCase : ICreateWalletUseCase
    {
        private readonly IWalletRepository _walletRepository;

        public CreateWalletUseCase(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public Task<ResponseCreateWalletDto> ExecuteAsync(RequestCreateWalletDto requestCreateWalletDto)
        {
            throw new NotImplementedException();
        }
    }
}
