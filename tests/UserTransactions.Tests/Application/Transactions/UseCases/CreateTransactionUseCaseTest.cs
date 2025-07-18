using FluentAssertions;
using UserTransactions.Application.UseCases.Transaction.Create;
using UserTransactions.Domain.Repositories.Transaction;
using UserTransactions.Domain.Repositories.Wallet;
using UserTransactions.Exception.Exceptions;
using UserTransactions.Tests.Shared.Builders.Dtos.Request.Transactions;
using UserTransactions.Tests.Shared.Builders.Repositories;

namespace UserTransactions.Tests.Application.Transactions.UseCases
{
    public class CreateTransactionUseCaseTest
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly ICreateTransactionUseCase _sut;

        public CreateTransactionUseCaseTest()
        {
            _transactionRepository = TransactionRepositoryBuilder.Build();
            _walletRepository = WalletRepositoryBuilder.Build();
            _sut = new CreateTransactionUseCase(_transactionRepository, _walletRepository);
        }

        [Fact]
        public async Task Given_ValidRequest_When_ExecuteAsyncIsCalled_Then_ShouldReturnValidResult()
        {
            // Arrange
            var request = RequestCreateTransactionDtoBuilder.Build();
            WalletRepositoryBuilder.SetupExistsByIdAsync(request.SenderId, true);
            WalletRepositoryBuilder.SetupExistsByIdAsync(request.ReceiverId, true);

            // Act
            var result = await _sut.ExecuteAsync(request);

            // Assert
            result.Should().NotBeNull();

            result.Id.Should().NotBeEmpty();
            result.Amount.Should().Be(request.Amount);
        }

        [Fact]
        public async Task Given_SenderNotRegistered_When_ExecuteAsyncIsCalled_Then_ShouldThrowErrorOnValidationException()
        {
            // Arrange
            var request = RequestCreateTransactionDtoBuilder.Build();
            WalletRepositoryBuilder.SetupExistsByIdAsync(request.SenderId, false);
            WalletRepositoryBuilder.SetupExistsByIdAsync(request.ReceiverId, true);

            // Act
            Func<Task> act = async () => await _sut.ExecuteAsync(request);

            // Assert
            await act.Should().ThrowAsync<ErrorOnValidationException>();
        }

        [Fact]
        public async Task Given_ReceiverNotRegistered_When_ExecuteAsyncIsCalled_Then_ShouldThrowErrorOnValidationException()
        {
            // Arrange
            var request = RequestCreateTransactionDtoBuilder.Build();
            WalletRepositoryBuilder.SetupExistsByIdAsync(request.SenderId, true);
            WalletRepositoryBuilder.SetupExistsByIdAsync(request.ReceiverId, false);

            // Act
            Func<Task> act = async () => await _sut.ExecuteAsync(request);

            // Assert
            await act.Should().ThrowAsync<ErrorOnValidationException>();
        }
    }
}
