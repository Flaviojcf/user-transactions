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

        public static IList<ResponseListAllWalletsDto> MapListAllFromWallet(this IList<WalletEntity> wallets)
        {
            return wallets.Select(wallet => new ResponseListAllWalletsDto
            {
                Id = wallet.Id,
                FullName = wallet.User!.FullName,
                Email = wallet.User!.Email,
                UserType = wallet.User.UserType,
                Balance = wallet.Balance
            }).ToList();
        }
    }
}