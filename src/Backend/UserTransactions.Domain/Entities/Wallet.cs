using UserTransactions.Domain.Exceptions;

namespace UserTransactions.Domain.Entities
{
    public class Wallet : BaseEntity
    {
        public Wallet(Guid userId, decimal initialBalance)
        {
            UserId = userId;
            Balance = initialBalance;
        }

        //Todo: Repassar mensagem para Designer
        public void Debit(decimal amount)
        {
            if (Balance <= 0) throw new DomainException("Saldo insuficiente.");
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