using UserTransactions.Communication.Dtos.Transaction.Request;

namespace UserTransactions.Tests.Shared.Builders.Dtos.Request.Transactions
{
    public static class RequestCreateTransactionDtoBuilder
    {
        public static RequestCreateTransactionDto Build()
        {
            return new RequestCreateTransactionDto
            {
                SenderId = Guid.NewGuid(),
                ReceiverId = Guid.NewGuid(),
                Amount = (decimal)100.00
            };
        }
    }
}
