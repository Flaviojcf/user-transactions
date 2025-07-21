using UserTransactions.Application.Mappers.Transaction;
using UserTransactions.Communication.Dtos.Transaction.Response;
using UserTransactions.Domain.Repositories.Transaction;

namespace UserTransactions.Application.UseCases.Transaction.ListAll
{
    public class ListAllTransactionUseCase : IListAllTransactionUseCase
    {
        private readonly ITransactionRepository _transactionRepository;

        public ListAllTransactionUseCase(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<IList<ResponseListTransactionsDto>> ExecuteAsync()
        {
            var transactions = await _transactionRepository.ListAllAsync();

            var responseListTransactionsDto = transactions.MapListAllFromTransactions();

            return responseListTransactionsDto;
        }
    }
}
