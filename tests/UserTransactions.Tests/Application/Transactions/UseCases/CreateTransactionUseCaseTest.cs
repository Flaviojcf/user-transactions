using FluentAssertions;
using UserTransactions.Application.UseCases.Transaction.Create;
using UserTransactions.Domain.Repositories;
using UserTransactions.Domain.Repositories.Transaction;
using UserTransactions.Domain.Repositories.Wallet;
using UserTransactions.Domain.Services.Authorize;
using UserTransactions.Domain.Services.Messaging;
using UserTransactions.Exception.Exceptions;
using UserTransactions.Tests.Shared.Builders.Dtos.Request.Transactions;
using UserTransactions.Tests.Shared.Builders.Entities;
using UserTransactions.Tests.Shared.Builders.Repositories;
using UserTransactions.Tests.Shared.Builders.Services;

namespace UserTransactions.Tests.Application.Transactions.UseCases
{
    public class CreateTransactionUseCaseTest
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;
        private readonly IKafkaMessageProducer _messageProducer;
        private readonly IAuthorizeService _authorizeService;
        private readonly ICreateTransactionUseCase _sut;

        public CreateTransactionUseCaseTest()
        {
            _transactionRepository = TransactionRepositoryBuilder.Build();
            _walletRepository = WalletRepositoryBuilder.Build();
            _unitOfWorkRepository = UnitOfWorkRepositoryBuilder.Build();
            _messageProducer = KafkaMessageProducerBuilder.Build();
            _authorizeService = AuthorizeServiceBuilder.Build();
            _sut = new CreateTransactionUseCase(_transactionRepository, _walletRepository, _unitOfWorkRepository, _authorizeService, _messageProducer);
        }

        [Fact]
        public async Task Given_ValidRequest_When_ExecuteAsyncIsCalled_Then_ShouldReturnValidResult()
        {
            // Arrange
            var request = RequestCreateTransactionDtoBuilder.Build();
            var senderUser = UserEntityBuilder.BuildUser();
            var receiverUser = UserEntityBuilder.BuildUser();
            var senderWallet = WalletEntityBuilder.Build();
            var receiverWallet = WalletEntityBuilder.Build();

            senderWallet.SetUser(senderUser);
            receiverWallet.SetUser(receiverUser);

            WalletRepositoryBuilder.SetupGetByIdAsync(request.SenderId, senderWallet);
            WalletRepositoryBuilder.SetupGetByIdAsync(request.ReceiverId, receiverWallet);
            WalletRepositoryBuilder.SetupUpdateAsync();
            TransactionRepositoryBuilder.SetupAddAsync();
            UnitOfWorkRepositoryBuilder.SetupTransactionMethods();
            AuthorizeServiceBuilder.SetupValidateAuthorizeService();

            KafkaMessageProducerBuilder.SetupPublishAsync<object>();

            // Act
            var result = await _sut.ExecuteAsync(request);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().NotBeEmpty();
            result.Amount.Should().Be(request.Amount);
        }

        [Fact]
        public async Task Given_ValidTransaction_When_ExecuteAsyncIsCalled_Then_SenderBalanceShouldBeDebited()
        {
            // Arrange
            var request = RequestCreateTransactionDtoBuilder.Build();
            var senderUser = UserEntityBuilder.BuildUser();
            var receiverUser = UserEntityBuilder.BuildUser();
            var senderWallet = WalletEntityBuilder.Build();
            var receiverWallet = WalletEntityBuilder.Build();

            senderWallet.SetUser(senderUser);
            receiverWallet.SetUser(receiverUser);

            var initialSenderBalance = senderWallet.Balance;

            WalletRepositoryBuilder.SetupGetByIdAsync(request.SenderId, senderWallet);
            WalletRepositoryBuilder.SetupGetByIdAsync(request.ReceiverId, receiverWallet);
            WalletRepositoryBuilder.SetupUpdateAsync();
            TransactionRepositoryBuilder.SetupAddAsync();
            UnitOfWorkRepositoryBuilder.SetupTransactionMethods();
            AuthorizeServiceBuilder.SetupValidateAuthorizeService();

            KafkaMessageProducerBuilder.SetupPublishAsync<object>();

            // Act
            await _sut.ExecuteAsync(request);

            // Assert
            senderWallet.Balance.Should().Be(initialSenderBalance - request.Amount);
        }

        [Fact]
        public async Task Given_ValidTransaction_When_ExecuteAsyncIsCalled_Then_ReceiverBalanceShouldBeCredited()
        {
            // Arrange
            var request = RequestCreateTransactionDtoBuilder.Build();
            var senderUser = UserEntityBuilder.BuildUser();
            var receiverUser = UserEntityBuilder.BuildUser();
            var senderWallet = WalletEntityBuilder.Build();
            var receiverWallet = WalletEntityBuilder.Build();

            senderWallet.SetUser(senderUser);
            receiverWallet.SetUser(receiverUser);

            var initialReceiverBalance = receiverWallet.Balance;

            WalletRepositoryBuilder.SetupGetByIdAsync(request.SenderId, senderWallet);
            WalletRepositoryBuilder.SetupGetByIdAsync(request.ReceiverId, receiverWallet);
            WalletRepositoryBuilder.SetupUpdateAsync();
            TransactionRepositoryBuilder.SetupAddAsync();
            UnitOfWorkRepositoryBuilder.SetupTransactionMethods();
            AuthorizeServiceBuilder.SetupValidateAuthorizeService();

            KafkaMessageProducerBuilder.SetupPublishAsync<object>();

            // Act
            await _sut.ExecuteAsync(request);

            // Assert
            receiverWallet.Balance.Should().Be(initialReceiverBalance + request.Amount);
        }

        [Fact]
        public async Task Given_ValidTransaction_When_ExecuteAsyncIsCalled_Then_BothWalletsBalancesShouldBeUpdatedCorrectly()
        {
            // Arrange
            var request = RequestCreateTransactionDtoBuilder.Build();
            var senderUser = UserEntityBuilder.BuildUser();
            var receiverUser = UserEntityBuilder.BuildUser();
            var senderWallet = WalletEntityBuilder.Build();
            var receiverWallet = WalletEntityBuilder.Build();

            senderWallet.SetUser(senderUser);
            receiverWallet.SetUser(receiverUser);

            var initialSenderBalance = senderWallet.Balance;
            var initialReceiverBalance = receiverWallet.Balance;
            var totalInitialBalance = initialSenderBalance + initialReceiverBalance;

            WalletRepositoryBuilder.SetupGetByIdAsync(request.SenderId, senderWallet);
            WalletRepositoryBuilder.SetupGetByIdAsync(request.ReceiverId, receiverWallet);
            WalletRepositoryBuilder.SetupUpdateAsync();
            TransactionRepositoryBuilder.SetupAddAsync();
            UnitOfWorkRepositoryBuilder.SetupTransactionMethods();

            KafkaMessageProducerBuilder.SetupPublishAsync<object>();

            // Act
            await _sut.ExecuteAsync(request);

            // Assert
            senderWallet.Balance.Should().Be(initialSenderBalance - request.Amount);
            receiverWallet.Balance.Should().Be(initialReceiverBalance + request.Amount);

            var totalFinalBalance = senderWallet.Balance + receiverWallet.Balance;
            totalFinalBalance.Should().Be(totalInitialBalance);
        }

        [Theory]
        [InlineData(50)]
        [InlineData(100)]
        [InlineData(250)]
        [InlineData(499)]
        public async Task Given_ValidTransactionWithSpecificAmount_When_ExecuteAsyncIsCalled_Then_BalancesShouldBeUpdatedWithCorrectAmount(decimal transactionAmount)
        {
            // Arrange
            var request = RequestCreateTransactionDtoBuilder.Build();
            request.Amount = transactionAmount;

            var senderUser = UserEntityBuilder.BuildUser();
            var receiverUser = UserEntityBuilder.BuildUser();
            var senderWallet = WalletEntityBuilder.Build();
            var receiverWallet = WalletEntityBuilder.Build();

            senderWallet.SetUser(senderUser);
            receiverWallet.SetUser(receiverUser);

            var initialSenderBalance = senderWallet.Balance;
            var initialReceiverBalance = receiverWallet.Balance;

            WalletRepositoryBuilder.SetupGetByIdAsync(request.SenderId, senderWallet);
            WalletRepositoryBuilder.SetupGetByIdAsync(request.ReceiverId, receiverWallet);
            WalletRepositoryBuilder.SetupUpdateAsync();
            TransactionRepositoryBuilder.SetupAddAsync();
            UnitOfWorkRepositoryBuilder.SetupTransactionMethods();

            KafkaMessageProducerBuilder.SetupPublishAsync<object>();

            // Act
            await _sut.ExecuteAsync(request);

            // Assert
            senderWallet.Balance.Should().Be(initialSenderBalance - transactionAmount);
            receiverWallet.Balance.Should().Be(initialReceiverBalance + transactionAmount);
        }

        [Fact]
        public async Task Given_SenderNotRegistered_When_ExecuteAsyncIsCalled_Then_ShouldThrowErrorOnValidationException()
        {
            // Arrange
            var request = RequestCreateTransactionDtoBuilder.Build();
            WalletRepositoryBuilder.SetupGetByIdAsync(request.SenderId, null);

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
            var senderUser = UserEntityBuilder.BuildUser();
            var senderWallet = WalletEntityBuilder.Build();
            senderWallet.SetUser(senderUser);

            WalletRepositoryBuilder.SetupGetByIdAsync(request.SenderId, senderWallet);
            WalletRepositoryBuilder.SetupGetByIdAsync(request.ReceiverId, null);

            // Act
            Func<Task> act = async () => await _sut.ExecuteAsync(request);

            // Assert
            await act.Should().ThrowAsync<ErrorOnValidationException>();
        }

        [Fact]
        public async Task Given_SenderUserTypeIsMerchant_When_ExecuteAsyncIsCalled_Then_ShouldThrowDomainException()
        {
            // Arrange
            var request = RequestCreateTransactionDtoBuilder.Build();
            var senderUser = UserEntityBuilder.BuildMerchant();
            var receiverUser = UserEntityBuilder.BuildUser();
            var senderWallet = WalletEntityBuilder.Build();
            var receiverWallet = WalletEntityBuilder.Build();

            senderWallet.SetUser(senderUser);
            receiverWallet.SetUser(receiverUser);

            WalletRepositoryBuilder.SetupGetByIdAsync(request.SenderId, senderWallet);
            WalletRepositoryBuilder.SetupGetByIdAsync(request.ReceiverId, receiverWallet);

            // Act
            Func<Task> act = async () => await _sut.ExecuteAsync(request);

            // Assert
            await act.Should().ThrowAsync<DomainException>();
        }

        [Fact]
        public async Task Given_AuthorizationServiceReturnsFailure_When_ExecuteAsyncIsCalled_Then_ShouldThrowErrorOnValidationException()
        {
            // Arrange
            var request = RequestCreateTransactionDtoBuilder.Build();
            var senderUser = UserEntityBuilder.BuildUser();
            var receiverUser = UserEntityBuilder.BuildUser();
            var senderWallet = WalletEntityBuilder.Build();
            var receiverWallet = WalletEntityBuilder.Build();

            senderWallet.SetUser(senderUser);
            receiverWallet.SetUser(receiverUser);

            WalletRepositoryBuilder.SetupGetByIdAsync(request.SenderId, senderWallet);
            WalletRepositoryBuilder.SetupGetByIdAsync(request.ReceiverId, receiverWallet);
            WalletRepositoryBuilder.SetupUpdateAsync();
            TransactionRepositoryBuilder.SetupAddAsync();
            UnitOfWorkRepositoryBuilder.SetupTransactionMethods();
            AuthorizeServiceBuilder.SetupValidateAuthorizeServiceThrowsException();


            // Act
            Func<Task> act = async () => await _sut.ExecuteAsync(request);

            // Assert
            await act.Should().ThrowAsync<ErrorOnValidationException>();
        }
    }
}