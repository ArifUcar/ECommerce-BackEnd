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

            // Seed Data
            builder.HasData(
                new OrderStatus 
                { 
                    Id = Guid.NewGuid(), 
                    Name = "Beklemede", 
                    Description = "Sipariş onay bekliyor",
                    DisplayOrder = 1,
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    IsActive = true
                },
                new OrderStatus 
                { 
                    Id = Guid.NewGuid(), 
                    Name = "Onaylandı", 
                    Description = "Sipariş onaylandı",
                    DisplayOrder = 2,
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    IsActive = true
                },
                new OrderStatus 
                { 
                    Id = Guid.NewGuid(), 
                    Name = "Hazırlanıyor", 
                    Description = "Sipariş hazırlanıyor",
                    DisplayOrder = 3,
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    IsActive = true
                },
                new OrderStatus 
                { 
                    Id = Guid.NewGuid(), 
                    Name = "Kargoda", 
                    Description = "Sipariş kargoya verildi",
                    DisplayOrder = 4,
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    IsActive = true
                },
                new OrderStatus 
                { 
                    Id = Guid.NewGuid(), 
                    Name = "Tamamlandı", 
                    Description = "Sipariş tamamlandı",
                    DisplayOrder = 5,
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    IsActive = true
                },
                new OrderStatus 
                { 
                    Id = Guid.NewGuid(), 
                    Name = "İptal Edildi", 
                    Description = "Sipariş iptal edildi",
                    DisplayOrder = 6,
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    IsActive = true
                }
            );
        }
    }
}
