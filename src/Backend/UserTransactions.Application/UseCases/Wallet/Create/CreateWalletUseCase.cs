using UserTransactions.Application.Mappers.Wallet;
using UserTransactions.Communication.Dtos.Wallet.Request;
using UserTransactions.Communication.Dtos.Wallet.Response;
using UserTransactions.Domain.Repositories.User;
using UserTransactions.Domain.Repositories.Wallet;
using UserTransactions.Exception;
using UserTransactions.Exception.Exceptions;

namespace UserTransactions.Application.UseCases.Wallet.Create
{
    public class CreateWalletUseCase : ICreateWalletUseCase
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IUserRepository _userRepository;

        public CreateWalletUseCase(IWalletRepository walletRepository, IUserRepository userRepository)
        {
            _walletRepository = walletRepository;
            _userRepository = userRepository;
        }

        public async Task<ResponseCreateWalletDto> ExecuteAsync(RequestCreateWalletDto requestCreateWalletDto)
        {
            await ValidateAsync(requestCreateWalletDto.UserId);

            var wallet = requestCreateWalletDto.MapToWallet();

            await _walletRepository.AddAsync(wallet);

            var responseCreateWalletDto = wallet.MapFromWallet();

            return responseCreateWalletDto;
        }

        private async Task ValidateAsync(Guid userId)
        {
            var userExistsAndIsActive = await _userRepository.ExistsAndIsActiveAsync(userId);

            if (!userExistsAndIsActive)
                throw new ErrorOnValidationException([ResourceMessagesException.UserNotRegistered]);

            var userAlreadyHasWallet = await _walletRepository.ExistsByUserIdAsync(userId);

            if (userAlreadyHasWallet)
                throw new ErrorOnValidationException([ResourceMessagesException.UserAlreadyHasWallet]);
        }
    }
}
