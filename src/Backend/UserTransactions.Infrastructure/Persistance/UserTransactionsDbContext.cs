using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using UserTransactions.Domain.Entities;

namespace UserTransactions.Infrastructure.Persistance
{
    [ExcludeFromCodeCoverage]
    public class UserTransactionsDbContext : DbContext
    {
        public UserTransactionsDbContext(DbContextOptions<UserTransactionsDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> Wallets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
