using AU_Framework.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AU_Framework.Persistance.Configurations;

public sealed class ProductDetailConfiguration : IEntityTypeConfiguration<ProductDetail>
{
    public void Configure(EntityTypeBuilder<ProductDetail> builder)
    {
        // Tablo adı
        builder.ToTable("ProductDetails");

        // Primary Key
        builder.HasKey(x => x.Id);

        // Foreign Key
        builder.HasOne(pd => pd.Product)
            .WithOne(p => p.ProductDetail)
            .HasForeignKey<ProductDetail>(pd => pd.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        // Property configurations
        builder.Property(x => x.Color)
            .HasColumnType("nvarchar(50)");

        builder.Property(x => x.Size)
            .HasColumnType("nvarchar(50)");

        builder.Property(x => x.Material)
            .HasColumnType("nvarchar(100)");

        builder.Property(x => x.Brand)
            .HasColumnType("nvarchar(100)");

        builder.Property(x => x.Model)
            .HasColumnType("nvarchar(100)");

        builder.Property(x => x.Warranty)
            .HasColumnType("nvarchar(100)");

        builder.Property(x => x.Specifications)
            .HasColumnType("nvarchar(max)");  // JSON verisi için

        builder.Property(x => x.AdditionalInformation)
            .HasColumnType("nvarchar(max)");

        builder.Property(x => x.Weight)
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.WeightUnit)
            .HasColumnType("nvarchar(20)");

        builder.Property(x => x.Dimensions)
            .HasColumnType("nvarchar(50)");

        builder.Property(x => x.StockCode)
            .HasColumnType("int");

        builder.Property(x => x.Barcode)
            .HasColumnType("nvarchar(50)");

        // Index'ler
        builder.HasIndex(x => x.ProductId)
            .IsUnique();

        builder.HasIndex(x => x.StockCode);
        builder.HasIndex(x => x.Barcode);

        // Audit properties
        builder.Property(x => x.CreatedDate)
            .IsRequired();

        builder.Property(x => x.UpdatedDate)
            .IsRequired();

        builder.Property(x => x.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);
    }
} 