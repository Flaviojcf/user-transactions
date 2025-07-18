using UserTransactions.Application.Mappers.Transaction;
using UserTransactions.Communication.Dtos.Transaction.Request;
using UserTransactions.Communication.Dtos.Transaction.Response;
using UserTransactions.Domain.Repositories.Transaction;
using UserTransactions.Domain.Repositories.Wallet;
using UserTransactions.Exception;
using UserTransactions.Exception.Exceptions;
using TransactionEntity = UserTransactions.Domain.Entities.Transaction;


namespace UserTransactions.Application.UseCases.Transaction.Create
{
    //TODO: Criar teses unitários para conferir saldo após as transações
    public class CreateTransactionUseCase : ICreateTransactionUseCase
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IWalletRepository _walletRepository;

        public CreateTransactionUseCase(ITransactionRepository transactionRepository, IWalletRepository walletRepository)
        {
            _transactionRepository = transactionRepository;
            _walletRepository = walletRepository;
        }

        public async Task<ResponseCreateTransactionDto> ExecuteAsync(RequestCreateTransactionDto request)
        {
            var transaction = request.MapToTransaction();

            await ValidateAsync(transaction);

            //Todo: Adicionar try/catch com transaction para credit e debit das contas
            await _transactionRepository.AddAsync(transaction);

            var responseCreateTransactionDto = transaction.MapFromTransaction();

            return responseCreateTransactionDto;
        }

        private async Task ValidateAsync(TransactionEntity transaction)
        {
            var existsSenderWallet = await _walletRepository.ExistsByIdAsync(transaction.SenderId);

            if (existsSenderWallet is false)
            {
                throw new ErrorOnValidationException([ResourceMessagesException.WalletSenderNotFound]);
            }

            var existsReceiverWallet = await _walletRepository.ExistsByIdAsync(transaction.ReceiverId);

            if (existsReceiverWallet is false)
            {
                throw new ErrorOnValidationException([ResourceMessagesException.WalletReceiverNotFound]);
            }
        }
    }
}
