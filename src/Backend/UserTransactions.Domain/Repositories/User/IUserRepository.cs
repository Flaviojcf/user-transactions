using UserEntity = UserTransactions.Domain.Entities.User;

namespace UserTransactions.Domain.Repositories.User
{
    public interface IUserRepository
    {
        Task AddAsync(UserEntity user);
        Task<bool> IsEmailAlreadyRegistered(string email);
        Task<bool> IsCpfAlreadyRegistered(string cpf);
        Task<bool> ExistsAndIsActiveAsync(Guid userId);
    }
}
