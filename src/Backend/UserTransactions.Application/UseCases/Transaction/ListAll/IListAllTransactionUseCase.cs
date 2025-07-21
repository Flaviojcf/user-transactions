using UserTransactions.Communication.Dtos.Transaction.Response;

namespace UserTransactions.Application.UseCases.Transaction.ListAll
{
    public interface IListAllTransactionUseCase
    {
        Task<IList<ResponseListTransactionsDto>> ExecuteAsync();
    }
}
