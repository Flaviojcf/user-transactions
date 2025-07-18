using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using UserTransactions.Domain.Entities;
using UserTransactions.Domain.Repositories.Wallet;

namespace UserTransactions.Infrastructure.Persistance.Repositories
{
    //Todo: Implementar testes unitários, fazendo in memory database para o DbContext e mockando o repositório.
    [ExcludeFromCodeCoverage]
    public class WalletRepository : IWalletRepository
    {
        private readonly UserTransactionsDbContext _dbContext;

        public WalletRepository(UserTransactionsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Wallet wallet)
        {
            await _dbContext.Wallets.AddAsync(wallet);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsByUserIdAsync(Guid userId)
        {
            return await _dbContext.Wallets
                .AnyAsync(w => w.UserId == userId && w.IsActive);
        }
    }
}