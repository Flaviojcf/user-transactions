using UserTransactions.Communication.Dtos.Transaction.Response;
using UserTransactions.Domain.Repositories.Transaction;

namespace UserTransactions.Application.UseCases.Transaction.ListTotalAmount
{
    public class ListTotalAmountTransactionUseCase : IListTotalAmountTransactionUseCase
    {
        private readonly ITransactionRepository _transactionRepository;

        public ListTotalAmountTransactionUseCase(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<ResponseTotalAmountTransactionsDto> ExecuteAsync()
        {
            var totalAmount = await _transactionRepository.ListTotalAmountAsync();

            return new ResponseTotalAmountTransactionsDto
            {
                TotalAmount = totalAmount
            };
        }
    }
}
