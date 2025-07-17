using FluentValidation;
using UserTransactions.Communication.Dtos.User.Request;
using UserTransactions.Exception;

namespace UserTransactions.Application.UseCases.User.Create
{
    public class CreateUserValidator : AbstractValidator<RequestCreateUserDto>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage(ResourceMessagesException.FullNameRequired);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(ResourceMessagesException.EmailRequired);

            RuleFor(x => x.CPF)
                .NotEmpty().WithMessage(ResourceMessagesException.CpfRequired);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(ResourceMessagesException.PasswordRequired);

            RuleFor(x => x.UserType)
                .IsInEnum().WithMessage(ResourceMessagesException.InvalidUserType);

            When(user => !string.IsNullOrEmpty(user.Email), () =>
            {
                RuleFor(x => x.Email)
                    .EmailAddress().WithMessage(ResourceMessagesException.InvalidEmailFormat);
            });

            When(user => !string.IsNullOrEmpty(user.CPF), () =>
            {
                RuleFor(x => x.CPF)
                    .Matches(@"^(\d{3})[\.\s]?(\d{3})[\.\s]?(\d{3})[-\s]?(\d{2})$").WithMessage(ResourceMessagesException.InvalidCpfFormat);
            });

            When(user => !string.IsNullOrEmpty(user.Password), () =>
            {
                RuleFor(x => x.Password)
                    .MinimumLength(6).WithMessage(ResourceMessagesException.InvalidPassword);
            });
        }
    }
}
