using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using UserTransactions.Application.UseCases.Health.GetOverallHealth;
using UserTransactions.Application.UseCases.Transaction.Create;
using UserTransactions.Application.UseCases.Transaction.ListAll;
using UserTransactions.Application.UseCases.Transaction.ListLatestFourTransactions;
using UserTransactions.Application.UseCases.Transaction.ListTotal;
using UserTransactions.Application.UseCases.Transaction.ListTotalAmount;
using UserTransactions.Application.UseCases.User.Create;
using UserTransactions.Application.UseCases.User.ListTotal;
using UserTransactions.Application.UseCases.Wallet.Create;
using UserTransactions.Application.UseCases.Wallet.ListTotal;
using UserTransactions.Communication.Dtos.Errors.Response;

namespace UserTransactions.Application.DI
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddUseCases();
            services.AddValidation();
        }

        public static void AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<ICreateUserUseCase, CreateUserUseCase>();
            services.AddScoped<ICreateWalletUseCase, CreateWalletUseCase>();
            services.AddScoped<ICreateTransactionUseCase, CreateTransactionUseCase>();


            services.AddScoped<IListAllTransactionUseCase, ListAllTransactionUseCase>();
            services.AddScoped<IListTotalQuantityTransactionUseCase, ListTotalQuantityTransactionUseCase>();
            services.AddScoped<IListTotalAmountTransactionUseCase, ListTotalAmountTransactionUseCase>();
            services.AddScoped<IListListLatestFourTransactionsUseCase, ListLatestFourTransactionsUseCase>();


            services.AddScoped<IListTotalQuantityUserUseCase, ListTotalQuantityUserUseCase>();


            services.AddScoped<IListTotalQuantityWalletUseCase, ListTotalQuantityWalletUseCase>();

            services.AddScoped<IGetOverallHealthUseCase, GetOverallHealthUseCase>();
        }

        private static void AddValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Values
                        .SelectMany(x => x.Errors)
                        .Select(x => x.ErrorMessage)
                        .Distinct()
                        .ToList();

                    var result = new ResponseErrorDto(errors, HttpStatusCode.BadRequest);
                    return new BadRequestObjectResult(result);
                };
            });
        }
    }
}
