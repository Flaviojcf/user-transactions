using UserTransactions.Communication.Dtos.Wallet.Request;
using UserTransactions.Communication.Dtos.Wallet.Response;
using WalletEntity = UserTransactions.Domain.Entities.Wallet;

namespace UserTransactions.Application.Mappers.Wallet
{
    public static class WalletMapper
    {
        public static WalletEntity MapToWallet(this RequestCreateWalletDto request)
        {
            return new WalletEntity(request.UserId);
        }

        public static ResponseCreateWalletDto MapFromWallet(this WalletEntity wallet)
        {
            return new ResponseCreateWalletDto
            {
                Id = wallet.Id,
                Balance = wallet.Balance
            };
        }
    }
}