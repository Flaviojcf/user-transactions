namespace UserTransactions.Domain.Repositories
{
    public interface IUnitOfWorkRepository
    {
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
        void Dispose();
    }
}