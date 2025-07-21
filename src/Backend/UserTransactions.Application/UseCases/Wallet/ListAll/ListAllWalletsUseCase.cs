using UserTransactions.Application.Mappers.Wallet;
using UserTransactions.Communication.Dtos.Wallet.Response;
using UserTransactions.Domain.Repositories.Wallet;

namespace UserTransactions.Application.UseCases.Wallet.ListAll
{
    public class ListAllWalletsUseCase : IListAllWalletsUseCase
    {
        private readonly IWalletRepository _walletRepository;

        public ListAllWalletsUseCase(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task<IList<ResponseListAllWalletsDto>> ExecuteAsync()
        {
            var wallets = await _walletRepository.ListAllAsync();

            var responseListAllWalletsDto = wallets.MapListAllFromWallet();

            return responseListAllWalletsDto;
        }
    }
}
