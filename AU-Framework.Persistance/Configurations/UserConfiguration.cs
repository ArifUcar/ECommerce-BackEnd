using AU_Framework.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AU_Framework.Persistance.Configurations
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.Password)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Phone)
                .HasMaxLength(20);

            builder.Property(x => x.Address)
                .HasMaxLength(500);

            builder.Property(x => x.RefreshToken)
                .HasMaxLength(500);

            builder.HasMany(x => x.Roles)
                .WithMany(x => x.Users)
                .UsingEntity(j => j.ToTable("UserRoles"));

            builder.HasMany(x => x.Orders)
                .WithOne()
                .HasForeignKey("UserId")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.HasIndex(x => x.Phone)
                .IsUnique()
                .HasFilter("[Phone] IS NOT NULL");
        }
    }
}
