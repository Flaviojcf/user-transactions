using UserTransactions.Application.UseCases.Wallet.Create;
using UserTransactions.Domain.Repositories.Wallet;
using UserTransactions.Tests.Shared.Builders.Repositories;

namespace UserTransactions.Tests.Application.Wallet.UseCases
{
    public class CreateWalletUseCaseTest
    {
        private readonly IWalletRepository _walletRepository;
        private readonly ICreateWalletUseCase _sut;

        public CreateWalletUseCaseTest()
        {
            _walletRepository = WalletRepositoryBuilder.Build();
            _sut = new CreateWalletUseCase(_walletRepository);
        }


    }
}
