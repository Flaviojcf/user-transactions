using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using UserTransactions.Domain.Entities;
using UserTransactions.Domain.Repositories.Transaction;

namespace UserTransactions.Infrastructure.Persistance.Repositories
{
    [ExcludeFromCodeCoverage]
    public class TransactionRepository : ITransactionRepository
    {
        private readonly UserTransactionsDbContext _dbContext;

        public TransactionRepository(UserTransactionsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Transaction transaction)
        {
            await _dbContext.Transactions.AddAsync(transaction);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IList<Transaction>> ListAllAsync()
        {
            return await _dbContext.Transactions
                .Include(t => t.Sender)
                    .ThenInclude(w => w!.User)
                .Include(t => t.Receiver)
                    .ThenInclude(w => w!.User)
                .ToListAsync();
        }

        public async Task<int> ListTotalAsync() => await _dbContext.Transactions.CountAsync();

        public async Task<decimal> ListTotalAmountAsync() => await _dbContext.Transactions.SumAsync(t => t.Amount);

        public async Task<IList<Transaction>> ListLatestFourAsync()
        {
            return await _dbContext.Transactions
                .Include(t => t.Sender)
                    .ThenInclude(w => w!.User)
                .Include(t => t.Receiver)
                    .ThenInclude(w => w!.User)
                .OrderByDescending(t => t.CreatedAt)
                .Take(4)
                .ToListAsync();
        }
    }
}
