using Moq;
using UserTransactions.Domain.Repositories.User;

namespace UserTransactions.Tests.Shared.Builders.Repositories
{
    public static class UserRepositoryBuilder
    {
        private static readonly Mock<IUserRepository> _mock = new Mock<IUserRepository>();

        public static IUserRepository Build() => _mock.Object;

        public static void SetupIsEmailAlreadyRegistered(string email)
        {
            _mock.Setup(repository => repository.IsEmailAlreadyRegistered(email)).ReturnsAsync(true);
        }

        public static void SetupIsCpfAlreadyRegistered(string cpf)
        {
            _mock.Setup(repository => repository.IsCpfAlreadyRegistered(cpf)).ReturnsAsync(true);
        }
    }
}
