using UserTransactions.Communication.Dtos.Transaction.Response;

namespace UserTransactions.Application.UseCases.Transaction.ListTotalAmount
{
    public interface IListTotalAmountTransactionUseCase
    {
        Task<ResponseTotalAmountTransactionsDto> ExecuteAsync();
    }
}
