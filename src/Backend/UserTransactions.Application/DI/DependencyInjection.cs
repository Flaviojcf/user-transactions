using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using UserTransactions.Application.UseCases.User.Create;

namespace UserTransactions.Application.DI
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddUseCases();
        }

        public static void AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<ICreateUserUseCase, CreateUserUseCase>();
        }
    }
}
