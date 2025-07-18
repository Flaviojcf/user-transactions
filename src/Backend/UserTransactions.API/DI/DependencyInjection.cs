using Asp.Versioning;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using UserTransactions.API.Filter;
using UserTransactions.Infrastructure.Persistance;

namespace UserTransactions.API.DI
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjection
    {
        public static void AddWebApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddVersioning();
            services.AddSqlServer(configuration);
            services.AddHealthChecks();
            services.AddLowerCaseUrl();
            services.AddFilters();
        }

        public static void AddVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),
                    new HeaderApiVersionReader("X-Api-Version")
                );
            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
        }

        public static void AddSqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UserTransactionsDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
        }

        private static void AddLowerCaseUrl(this IServiceCollection services)
        {
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
            });
        }

        private static void AddFilters(this IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add<TrimStringsActionFilter>();
                options.Filters.Add<ExceptionFilter>();
            });

        }
    }
}
