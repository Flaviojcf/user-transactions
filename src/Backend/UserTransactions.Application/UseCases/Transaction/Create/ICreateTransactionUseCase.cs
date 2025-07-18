using UserTransactions.Communication.Dtos.Transaction.Request;
using UserTransactions.Communication.Dtos.Transaction.Response;

namespace UserTransactions.Application.UseCases.Transaction.Create
{
    public interface ICreateTransactionUseCase
    {
        Task<ResponseCreateTransactionDto> ExecuteAsync(RequestCreateTransactionDto request);
    }
}
