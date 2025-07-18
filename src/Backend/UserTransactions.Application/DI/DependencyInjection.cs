using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using UserTransactions.Application.UseCases.Transaction.Create;
using UserTransactions.Application.UseCases.User.Create;
using UserTransactions.Application.UseCases.Wallet.Create;
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
