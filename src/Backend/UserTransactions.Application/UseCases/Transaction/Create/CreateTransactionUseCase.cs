using UserTransactions.Application.Mappers.Transaction;
using UserTransactions.Communication.Dtos.Transaction.Request;
using UserTransactions.Communication.Dtos.Transaction.Response;
using UserTransactions.Domain.Repositories;
using UserTransactions.Domain.Repositories.Transaction;
using UserTransactions.Domain.Repositories.Wallet;
using UserTransactions.Exception;
using UserTransactions.Exception.Exceptions;
using TransactionEntity = UserTransactions.Domain.Entities.Transaction;
using WalletEntity = UserTransactions.Domain.Entities.Wallet;


namespace UserTransactions.Application.UseCases.Transaction.Create
{
    //TODO: Criar teses unitários para conferir saldo após as transações
    public class CreateTransactionUseCase : ICreateTransactionUseCase
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly IUnitOfWorkRepository _unitOfWork;
        private readonly IHttpClientFactory _httpClientFactory;

        public CreateTransactionUseCase(ITransactionRepository transactionRepository, IWalletRepository walletRepository, IUnitOfWorkRepository unitOfWork, IHttpClientFactory httpClientFactory)
        {
            _transactionRepository = transactionRepository;
            _walletRepository = walletRepository;
            _unitOfWork = unitOfWork;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseCreateTransactionDto> ExecuteAsync(RequestCreateTransactionDto request)
        {
            var transaction = request.MapToTransaction();

            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var (senderWallet, receiverWallet) = await ValidateAsync(transaction);

                senderWallet.Debit(transaction.Amount);

                receiverWallet.Credit(transaction.Amount);

                await _walletRepository.UpdateAsync(senderWallet);
                await _walletRepository.UpdateAsync(receiverWallet);
                await _transactionRepository.AddAsync(transaction);
                await ValidateAuthorizeService();

                await _unitOfWork.CommitAsync();

                return transaction.MapFromTransaction();
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        private async Task<(WalletEntity senderWallet, WalletEntity receiverWallet)> ValidateAsync(TransactionEntity transaction)
        {
            var senderWallet = await _walletRepository.GetByIdAsync(transaction.SenderId);

            if (senderWallet is null)
            {
                throw new ErrorOnValidationException([ResourceMessagesException.WalletSenderNotFound]);
            }

            var receiverWallet = await _walletRepository.GetByIdAsync(transaction.ReceiverId);

            if (receiverWallet is null)
            {
                throw new ErrorOnValidationException([ResourceMessagesException.WalletReceiverNotFound]);
            }

            return (senderWallet, receiverWallet);
        }


        private async Task ValidateAuthorizeService()
        {
            using var httpClient = _httpClientFactory.CreateClient();

            var response = await httpClient.GetAsync("https://util.devi.tools/api/v2/authorize");

            if (!response.IsSuccessStatusCode)
            {
                throw new ErrorOnValidationException([ResourceMessagesException.TransactionNotAuthorized]);
            }
        }
    }
}
