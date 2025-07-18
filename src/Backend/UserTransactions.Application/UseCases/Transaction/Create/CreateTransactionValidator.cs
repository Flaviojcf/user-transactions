using FluentValidation;
using UserTransactions.Communication.Dtos.Transaction.Request;
using UserTransactions.Exception;

namespace UserTransactions.Application.UseCases.Transaction.Create
{
    public class CreateTransactionValidator : AbstractValidator<RequestCreateTransactionDto>
    {
        public CreateTransactionValidator()
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage(ResourceMessagesException.TransactionAmountMustBeGreaterThanZero);

            RuleFor(x => x.SenderId)
                .NotEmpty()
                .WithMessage(ResourceMessagesException.SenderIdRequired);

            RuleFor(x => x.ReceiverId)
                .NotEmpty()
                .WithMessage(ResourceMessagesException.ReceiverIdRequired);

            When(transaction => transaction.SenderId != Guid.Empty && transaction.ReceiverId != Guid.Empty, () =>
            {
                RuleFor(x => x)
                    .Must(x => x.SenderId != x.ReceiverId)
                    .WithMessage(ResourceMessagesException.SenderAndReceiverMustBeDifferent);
            });
        }
    }
}
