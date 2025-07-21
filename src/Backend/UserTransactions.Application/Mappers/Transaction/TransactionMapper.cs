using UserTransactions.Communication.Dtos.Transaction.Request;
using UserTransactions.Communication.Dtos.Transaction.Response;
using TransactionEntity = UserTransactions.Domain.Entities.Transaction;

namespace UserTransactions.Application.Mappers.Transaction
{
    public static class TransactionMapper
    {
        public static TransactionEntity MapToTransaction(this RequestCreateTransactionDto request)
        {
            return new TransactionEntity(request.SenderId, request.ReceiverId, request.Amount);
        }

        public static ResponseCreateTransactionDto MapFromTransaction(this TransactionEntity transaction)
        {
            return new ResponseCreateTransactionDto
            {
                Id = transaction.Id,
                Amount = transaction.Amount,
            };
        }

        public static IList<ResponseListTransactionsDto> MapListAllFromTransactions(this IList<TransactionEntity> transactions)
        {
            return transactions.Select(transaction => new ResponseListTransactionsDto
            {
                SenderName = transaction.Sender!.User!.FullName,
                ReceiverName = transaction.Receiver!.User!.FullName,
                Amount = transaction.Amount,
                CreatedAt = transaction.CreatedAt
            }).ToList();
        }
    }
}
