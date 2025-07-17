using Microsoft.EntityFrameworkCore;
using System.Reflection;
using UserTransactions.Domain.Entities;

namespace UserTransactions.Infrastructure.Persistance
{
    public class UserTransactionsDbContext : DbContext
    {
        public UserTransactionsDbContext(DbContextOptions<UserTransactionsDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
