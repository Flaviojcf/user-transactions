using UserTransactions.Application.Mappers.Transaction;
using UserTransactions.Communication.Dtos.Transaction.Response;
using UserTransactions.Domain.Repositories.Transaction;

namespace UserTransactions.Application.UseCases.Transaction.ListLatestFourTransactions
{
    public class ListLatestFourTransactionsUseCase : IListListLatestFourTransactionsUseCase
    {
        private readonly ITransactionRepository _transactionRepository;
        public ListLatestFourTransactionsUseCase(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<IList<ResponseListTransactionsDto>> ExecuteAsync()
        {
            var transactions = await _transactionRepository.ListLatestFourAsync();

            var responseListTransactionsDto = transactions.MapListAllFromTransactions();

            return responseListTransactionsDto;
        }
    }
}
