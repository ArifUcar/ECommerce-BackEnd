using AU_Framework.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AU_Framework.Persistance.Configurations;

public sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        // Primary key
        builder.HasKey(x => x.Id);

        // Property configurations
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Description)
            .HasMaxLength(200);

        // Seed data
        builder.HasData(
            new Role 
            { 
                Id = Guid.NewGuid(), 
                Name = "Admin", 
                Description = "Sistem Yöneticisi",
                CreatedDate = DateTime.UtcNow
            },
            new Role 
            { 
                Id = Guid.NewGuid(), 
                Name = "Manager", 
                Description = "Yönetici",
                CreatedDate = DateTime.UtcNow
            },
            new Role 
            { 
                Id = Guid.NewGuid(), 
                Name = "User", 
                Description = "Kullanıcı",
                CreatedDate = DateTime.UtcNow
            }
        );

        // Table name
        builder.ToTable("Roles");
    }
} 