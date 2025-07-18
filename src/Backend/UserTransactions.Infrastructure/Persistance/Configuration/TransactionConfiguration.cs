using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;
using UserTransactions.Domain.Entities;

namespace UserTransactions.Infrastructure.Persistance.Configuration
{
    [ExcludeFromCodeCoverage]
    public sealed class TransactionConfiguration : BaseConfiguration<Transaction>
    {
        public override void Configure(EntityTypeBuilder<Transaction> builder)
        {
            base.Configure(builder);

            builder.ToTable("Transactions");

            builder.Property(t => t.Amount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(t => t.SenderId)
                .IsRequired();

            builder.Property(t => t.ReceiverId)
                .IsRequired();

            builder.HasOne(t => t.Sender)
                .WithMany()
                .HasForeignKey(t => t.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.Receiver)
                .WithMany()
                .HasForeignKey(t => t.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(t => t.SenderId);
            builder.HasIndex(t => t.ReceiverId);
        }
    }
}
