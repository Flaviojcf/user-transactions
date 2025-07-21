using UserTransactions.Communication.Dtos.Transaction.Response;

namespace UserTransactions.Application.UseCases.Transaction.ListTotal
{
    public interface IListTotalQuantityTransactionUseCase
    {
        Task<ResponseTotalQuantityTransactionsDto> ExecuteAsync();
    }
}
