using FluentValidation;
using UserTransactions.Communication.Dtos.Wallet.Request;
using UserTransactions.Exception;

namespace UserTransactions.Application.UseCases.Wallet.Create
{
    public class CreateWalletValidator : AbstractValidator<RequestCreateWalletDto>
    {
        public CreateWalletValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage(ResourceMessagesException.UserIdRequired);
        }
    }
}
