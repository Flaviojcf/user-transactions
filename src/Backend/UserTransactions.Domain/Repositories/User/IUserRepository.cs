using UserEntity = UserTransactions.Domain.Entities.User;

namespace UserTransactions.Domain.Repositories.User
{
    public interface IUserRepository
    {
        Task AddAsync(UserEntity user);
    }
}
