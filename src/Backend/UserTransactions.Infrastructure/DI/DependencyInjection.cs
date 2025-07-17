using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using UserTransactions.Domain.Repositories.User;
using UserTransactions.Infrastructure.Persistance.Repositories;

namespace UserTransactions.Infrastructure.DI
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddRepositories();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}