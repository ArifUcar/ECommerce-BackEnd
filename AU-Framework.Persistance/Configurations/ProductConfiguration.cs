using AU_Framework.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AU_Framework.Persistance.Configurations
{
    public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProductName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.DiscountedPrice)
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.DiscountRate)
                .HasColumnType("decimal(5,2)");

            builder.Property(x => x.DiscountStartDate)
                .HasColumnType("datetime2");

            builder.Property(x => x.DiscountEndDate)
                .HasColumnType("datetime2");

            builder.Property(x => x.StockQuantity)
                .IsRequired();

            builder.Property(x => x.ImagePath)
                .HasMaxLength(500);

            builder.Property(x => x.Base64Image)
                .HasColumnType("nvarchar(max)");

            builder.HasOne(x => x.Category)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ProductDetail)
                .WithOne(x => x.Product)
                .HasForeignKey<ProductDetail>(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => x.ProductName);
            builder.HasIndex(x => x.CategoryId);
            builder.HasIndex(x => x.DiscountStartDate);
            builder.HasIndex(x => x.DiscountEndDate);
            builder.HasIndex(x => x.IsDeleted);

            builder.Property(x => x.CreatedDate)
                .IsRequired();

            builder.Property(x => x.UpdatedDate)
                .IsRequired();

            builder.Property(x => x.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);
        }
    }
}


