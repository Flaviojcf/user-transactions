
using UserTransactions.Communication.Dtos.Transaction.Response;
using UserTransactions.Domain.Repositories.Transaction;

namespace UserTransactions.Application.UseCases.Transaction.ListTotal
{
    public class ListTotalQuantityTransactionUseCase : IListTotalQuantityTransactionUseCase
    {
        private readonly ITransactionRepository _transactionRepository;

        public ListTotalQuantityTransactionUseCase(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<ResponseTotalQuantityTransactionsDto> ExecuteAsync()
        {
            var totalQuantity = await _transactionRepository.ListTotalAsync();

            return new ResponseTotalQuantityTransactionsDto
            {
                TotalQuantity = totalQuantity
            };
        }
    }
}
