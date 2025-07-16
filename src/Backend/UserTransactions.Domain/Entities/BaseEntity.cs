namespace UserTransactions.Domain.Entities
{
    public abstract class BaseEntity
    {
        protected BaseEntity() { }

        public void Activate()
        {
            IsActive = true;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Deactivate()
        {
            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
        }

        public Guid Id { get; private set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; private set; }
        public bool IsActive { get; private set; } = true;
    }
}
