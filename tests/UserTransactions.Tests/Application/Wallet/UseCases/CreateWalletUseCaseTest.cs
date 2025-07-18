using FluentAssertions;
using UserTransactions.Application.UseCases.Wallet.Create;
using UserTransactions.Domain.Repositories.User;
using UserTransactions.Domain.Repositories.Wallet;
using UserTransactions.Exception.Exceptions;
using UserTransactions.Tests.Shared.Builders.Dtos.Request.Wallet;
using UserTransactions.Tests.Shared.Builders.Repositories;

namespace UserTransactions.Tests.Application.Wallet.UseCases
{
    public class CreateWalletUseCaseTest
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICreateWalletUseCase _sut;

        public CreateWalletUseCaseTest()
        {
            _walletRepository = WalletRepositoryBuilder.Build();
            _userRepository = UserRepositoryBuilder.Build();
            _sut = new CreateWalletUseCase(_walletRepository, _userRepository);
        }

        [Fact]
        public async Task Given_ValidRequest_When_ExecuteAsyncIsCalled_Then_ShouldReturnValidResult()
        {
            // Arrange
            var request = RequestCreateWalletDtoBuilder.Build();
            WalletRepositoryBuilder.SetupAddAsync();
            UserRepositoryBuilder.SetupExistsAndIsActiveAsync(request.UserId, true);
            WalletRepositoryBuilder.SetupExistsByUserIdAsync(request.UserId, false);

            // Act
            var result = await _sut.ExecuteAsync(request);

            // Assert
            result.Should().NotBeNull();

            result.Id.Should().NotBeEmpty();
            result.Balance.Should().Be(500);

            WalletRepositoryBuilder.VerifyAddAsyncWasCalled();
        }

        [Fact]
        public async Task Given_ValidRequest_When_ExecuteAsyncIsCalled_Then_ShouldCallRepositoryAddAsync()
        {
            // Arrange
            var request = RequestCreateWalletDtoBuilder.Build();
            WalletRepositoryBuilder.SetupAddAsync();
            UserRepositoryBuilder.SetupExistsAndIsActiveAsync(request.UserId, true);
            WalletRepositoryBuilder.SetupExistsByUserIdAsync(request.UserId, false);

            // Act
            await _sut.ExecuteAsync(request);

            // Assert
            WalletRepositoryBuilder.VerifyAddAsyncWasCalled();
        }

        [Fact]
        public async Task Given_UserNotRegistered_When_ExecuteAsyncIsCalled_Then_ShouldThrowErrorOnValidationException()
        {
            // Arrange
            var request = RequestCreateWalletDtoBuilder.Build();
            UserRepositoryBuilder.SetupExistsAndIsActiveAsync(request.UserId, false);

            // Act
            Func<Task> act = async () => await _sut.ExecuteAsync(request);

            // Assert
            await act.Should().ThrowAsync<ErrorOnValidationException>();
        }

        [Fact]
        public async Task Given_UserAlreadyHasWallet_When_ExecuteAsyncIsCalled_Then_ShouldThrowErrorOnValidationException()
        {
            // Arrange
            var request = RequestCreateWalletDtoBuilder.Build();
            UserRepositoryBuilder.SetupExistsAndIsActiveAsync(request.UserId, true);
            WalletRepositoryBuilder.SetupExistsByUserIdAsync(request.UserId, true);

            // Act
            Func<Task> act = async () => await _sut.ExecuteAsync(request);

            // Assert
            await act.Should().ThrowAsync<ErrorOnValidationException>();
        }
    }
}
