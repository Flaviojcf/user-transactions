using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

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
            // Register application use cases here
        }
    }
}
