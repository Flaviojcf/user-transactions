using UserTransactions.Communication.Dtos.Wallet.Response;
using UserTransactions.Domain.Repositories.Wallet;

namespace UserTransactions.Application.UseCases.Wallet.ListTotal
{
    public class ListTotalQuantityWalletUseCase : IListTotalQuantityWalletUseCase
    {
        private readonly IWalletRepository _walletRepository;

        public ListTotalQuantityWalletUseCase(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task<ResponseListTotalQuantityWalletDto> ExecuteAsync()
        {
            var totalQuantity = await _walletRepository.ListTotalQuantityAsync();

            return new ResponseListTotalQuantityWalletDto
            {
                TotalQuantity = totalQuantity
            };
        }
    }
}
