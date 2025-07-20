using Asp.Versioning;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using UserTransactions.API.Filter;
using UserTransactions.Domain.Services.Messaging;
using UserTransactions.Infrastructure.Configuration;
using UserTransactions.Infrastructure.Persistance;
using UserTransactions.Infrastructure.Services.Messaging;

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
            services.AddHttp();
            services.AddKafka(configuration);
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
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                });
            });
        }

        public static void AddLowerCaseUrl(this IServiceCollection services)
        {
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
            });
        }

        public static void AddFilters(this IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add<TrimStringsActionFilter>();
                options.Filters.Add<ExceptionFilter>();
            });

        }

        public static void AddHttp(this IServiceCollection services)
        {
            services.AddHttpClient();
        }

        public static void AddKafka(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<KafkaOptions>(configuration.GetSection(KafkaOptions.SectionName));
            services.AddSingleton<KafkaProducerFactory>();
            services.AddScoped<IKafkaMessageProducer, KafkaMessageProducer>();
        }
    }
}
