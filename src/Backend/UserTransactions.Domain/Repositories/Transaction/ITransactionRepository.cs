using TransactionEntity = UserTransactions.Domain.Entities.Transaction;

namespace UserTransactions.Domain.Repositories.Transaction
{
    public interface ITransactionRepository
    {
        Task AddAsync(TransactionEntity transaction);
        Task<TransactionEntity?> GetByIdAsync(Guid id);
        Task<IEnumerable<TransactionEntity>> GetAllAsync();
    }
}
