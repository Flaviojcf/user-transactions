using UserTransactions.Domain.Enum;
using UserTransactions.Exception;
using UserTransactions.Exception.Exceptions;

namespace UserTransactions.Domain.Entities
{
    public sealed class Wallet : BaseEntity
    {
        public Wallet(Guid userId)
        {
            UserId = userId;
            Balance = 0;
        }

        public void Debit(decimal amount)
        {
            if (User!.UserType.Equals(UserType.Merchant)) throw new DomainException(ResourceMessagesException.MerchantCannotDebit);
            if (Balance <= 0) throw new DomainException(ResourceMessagesException.InsufficientBalance);
        }

        public void Credit(decimal amount)
        {
            Balance += amount;
        }

        public decimal Balance { get; private set; }

        public Guid UserId { get; private set; }
        public User? User { get; private set; }
    }
}