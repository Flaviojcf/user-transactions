using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;
using UserTransactions.Domain.Entities;

namespace UserTransactions.Infrastructure.Persistance.Configuration
{
    [ExcludeFromCodeCoverage]
    public sealed class WalletConfiguration : BaseConfiguration<Wallet>
    {
        public override void Configure(EntityTypeBuilder<Wallet> builder)
        {
            base.Configure(builder);

            builder.ToTable("Wallets");

            builder.Property(w => w.Balance)
                .IsRequired()
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0);

            builder.Property(w => w.UserId)
                .IsRequired();

            builder.HasOne(w => w.User)
                .WithMany()
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(w => w.UserId)
                .IsUnique();
        }
    }
}