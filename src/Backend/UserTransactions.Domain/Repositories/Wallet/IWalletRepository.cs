using WalletEntity = UserTransactions.Domain.Entities.Wallet;

namespace UserTransactions.Domain.Repositories.Wallet
{
    public interface IWalletRepository
    {
        Task AddAsync(WalletEntity wallet);
        Task<bool> ExistsByUserIdAsync(Guid userId);
        Task<bool> ExistsByIdAsync(Guid id);
        Task<WalletEntity?> GetByIdAsync(Guid id);
        Task UpdateAsync(WalletEntity wallet);
    }
}
