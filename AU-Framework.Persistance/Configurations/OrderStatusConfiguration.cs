using AU_Framework.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AU_Framework.Persistance.Configurations
{
    public sealed class OrderStatusConfiguration : IEntityTypeConfiguration<OrderStatus>
    {
        public void Configure(EntityTypeBuilder<OrderStatus> builder)
        {
            builder.ToTable("OrderStatuses");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Description)
                .HasMaxLength(250);

            builder.Property(x => x.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(x => x.DisplayOrder)
                .IsRequired()
                .HasDefaultValue(0);

            // Varsayılan sipariş durumlarını ekle
            builder.HasData(
                new OrderStatus 
                { 
                    Id = new Guid("8f7579ee-4af9-4b71-9ada-7f792f76921e"),
                    Name = "Beklemede",
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    IsActive = true,
                    DisplayOrder = 1
                },
                new OrderStatus 
                { 
                    Id = new Guid("9f7579ee-4af9-4b71-9ada-7f792f76921f"),
                    Name = "Onaylandı",
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    IsActive = true,
                    DisplayOrder = 2
                },
                new OrderStatus 
                { 
                    Id = new Guid("af7579ee-4af9-4b71-9ada-7f792f76921a"),
                    Name = "Hazırlanıyor",
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    IsActive = true,
                    DisplayOrder = 3
                },
                new OrderStatus 
                { 
                    Id = new Guid("bf7579ee-4af9-4b71-9ada-7f792f76921b"),
                    Name = "Kargoya Verildi",
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    IsActive = true,
                    DisplayOrder = 4
                },
                new OrderStatus 
                { 
                    Id = new Guid("cf7579ee-4af9-4b71-9ada-7f792f76921c"),
                    Name = "Tamamlandı",
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    IsActive = true,
                    DisplayOrder = 5
                },
                new OrderStatus 
                { 
                    Id = new Guid("df7579ee-4af9-4b71-9ada-7f792f76921d"),
                    Name = "İptal Edildi",
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    IsActive = true,
                    DisplayOrder = 6
                }
            );

            // Audit properties
            builder.Property(x => x.CreatedDate)
                .IsRequired();

            builder.Property(x => x.UpdatedDate)
                .IsRequired();

            builder.Property(x => x.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            // Index
            builder.HasIndex(x => x.Name)
                .IsUnique();
        }
    }
}
