using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserTransactions.Domain.Entities;

namespace UserTransactions.Infrastructure.Persistance.Configuration
{
    public sealed class UserConfiguration : BaseConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.ToTable("Users");

            builder.Property(u => u.FullName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.CPF)
                .IsRequired()
                .HasMaxLength(11);

            builder.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.UserType)
                .IsRequired()
                .HasConversion<string>();

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.HasIndex(u => u.CPF)
                .IsUnique();
        }
    }
}
