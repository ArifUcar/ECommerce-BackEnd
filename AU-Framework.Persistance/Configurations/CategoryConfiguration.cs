using AU_Framework.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AU_Framework.Persistance.Configurations
{
    // IEntityTypeConfiguration<Category> arayüzünü implement eder.
    // Bu sınıf, Category entity'sinin veritabanı yapılandırmasını tanımlar.
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        // Configure metodunda, Category entity'si için veritabanı konfigürasyonlarını ayarlıyoruz.
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // Category entity'sinin veritabanındaki tablo ismini belirliyoruz.
            builder.ToTable("Categories");

            // Category entity'si için birincil anahtar (Primary Key) olarak "Id" alanını ayarlıyoruz.
            builder.HasKey(c => c.Id);

            // CategoryName özelliği için veritabanı ayarlarını yapıyoruz.
            // Bu alanın zorunlu (Required) olmasını sağlıyoruz ve 
            // azami uzunluğunu 100 karakter olarak belirliyoruz.
            builder.Property(c => c.CategoryName)
                .IsRequired()      // Bu alanın boş geçilemez olduğunu belirler.
                .HasMaxLength(100); // Bu alanda en fazla 100 karakter saklanabileceğini belirtir.
        }
    }
}
