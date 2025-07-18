using Microsoft.EntityFrameworkCore.Storage;
using UserTransactions.Domain.Repositories;

namespace UserTransactions.Infrastructure.Persistance.Repositories
{
    public class UnitOfWork : IUnitOfWorkRepository
    {
        private readonly UserTransactionsDbContext _dbContext;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(UserTransactionsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                await _dbContext.SaveChangesAsync();

                if (_transaction != null)
                {
                    await _transaction.CommitAsync();
                }
            }
            catch
            {
                await RollbackAsync();
                throw;
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = null;
            }
        }

        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                _transaction.Dispose();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
        }
    }
}