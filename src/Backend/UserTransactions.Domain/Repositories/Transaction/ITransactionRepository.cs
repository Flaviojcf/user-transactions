using TransactionEntity = UserTransactions.Domain.Entities.Transaction;

namespace UserTransactions.Domain.Repositories.Transaction
{
    public interface ITransactionRepository
    {
        Task AddAsync(TransactionEntity transaction);
        Task<IList<TransactionEntity>> ListAllAsync();
        Task<int> ListTotalAsync();
        Task<decimal> ListTotalAmountAsync();
        Task<IList<TransactionEntity>> ListLatestFourAsync();
    }
}
