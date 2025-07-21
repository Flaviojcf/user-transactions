using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using UserTransactions.Domain.Entities;
using UserTransactions.Domain.Repositories.User;

namespace UserTransactions.Infrastructure.Persistance.Repositories
{
    //Todo: Implementar testes unitários, fazendo in memory database para o DbContext e mockando o repositório.
    [ExcludeFromCodeCoverage]
    public class UserRepository : IUserRepository
    {
        private readonly UserTransactionsDbContext _dbContext;

        public UserRepository(UserTransactionsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> IsCpfAlreadyRegistered(string cpf) => await _dbContext.Users.AnyAsync(u => u.CPF == cpf);

        public async Task<bool> IsEmailAlreadyRegistered(string email) => await _dbContext.Users.AnyAsync(u => u.Email == email);

        public async Task<bool> ExistsAndIsActiveAsync(Guid userId) => await _dbContext.Users.AnyAsync(u => u.Id == userId && u.IsActive);

        public async Task<int> ListTotalQuantityAsync() => await _dbContext.Users.CountAsync();

        public async Task<IList<User>> ListAllAsync() => await _dbContext.Users.ToListAsync();
    }
}
