using UserTransactions.Communication.Dtos.Transaction.Response;

namespace UserTransactions.Application.UseCases.Transaction.ListLatestFourTransactions
{
    public interface IListListLatestFourTransactionsUseCase
    {
        Task<IList<ResponseListTransactionsDto>> ExecuteAsync();
    }
}
