using AU_Framework.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AU_Framework.Persistance.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(255)
                .HasField("_password");

            builder.Property(u => u.Phone)
                .HasMaxLength(20);

            builder.Property(u => u.Address)
                .HasMaxLength(300);

            builder.Property(u => u.RefreshToken)
                .HasMaxLength(512)
                .IsRequired(false);

            builder.Property(u => u.RefreshTokenExpires)
                .IsRequired(false);

            builder.Property(u => u.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(u => u.LastLoginDate)
                .IsRequired()
                .HasDefaultValue(DateTime.UtcNow);

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.HasIndex(u => u.Phone)
                .IsUnique()
                .HasFilter("[Phone] IS NOT NULL");
        }
    }
}
