using UserTransactions.Domain.Entities;
using UserTransactions.Domain.Repositories.Transaction;

namespace UserTransactions.Infrastructure.Persistance.Repositories
{
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
    }
}
