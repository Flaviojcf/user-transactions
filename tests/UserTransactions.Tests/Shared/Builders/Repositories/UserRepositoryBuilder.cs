using Moq;
using UserTransactions.Domain.Repositories.User;
using UserEntity = UserTransactions.Domain.Entities.User;

namespace UserTransactions.Tests.Shared.Builders.Repositories
{
    public static class UserRepositoryBuilder
    {
        private static readonly Mock<IUserRepository> _mock = new Mock<IUserRepository>();

        public static IUserRepository Build() => _mock.Object;

        public static void SetupIsEmailAlreadyRegistered(string email)
        {
            _mock.Setup(x => x.IsEmailAlreadyRegistered(email)).ReturnsAsync(true);
        }

        public static void SetupIsCpfAlreadyRegistered(string cpf)
        {
            _mock.Setup(x => x.IsCpfAlreadyRegistered(cpf)).ReturnsAsync(true);
        }

        public static void SetupExistsAndIsActiveAsync(Guid userId, bool exists)
        {
            _mock.Setup(x => x.ExistsAndIsActiveAsync(userId)).ReturnsAsync(exists);
        }

        public static void SetupListTotalQuantityAsync(int totalQuantity)
        {
            _mock.Setup(x => x.ListTotalQuantityAsync()).ReturnsAsync(totalQuantity);
        }

        public static void SetupListAllAsync(IList<UserEntity> users)
        {
            _mock.Setup(x => x.ListAllAsync()).ReturnsAsync(users);
        }
    }
}
