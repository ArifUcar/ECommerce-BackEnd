using AU_Framework.Domain.Abstract;  
using Microsoft.EntityFrameworkCore;  

namespace AU_Framework.Persistance.Context
{
    // AppDbContext, Entity Framework Core'dan DbContext sınıfını devralarak veritabanı ile etkileşim sağlar.
    public sealed class AppDbContext : DbContext
    {
        // DbContext sınıfının yapıcısı (constructor) içerideki seçenekleri (DbContextOptions) alır ve üst sınıfa gönderir.
        public AppDbContext(DbContextOptions options) : base(options) { }

        // OnModelCreating, model yapılandırmalarını uygulamak için kullanılır.
        // Burada, yapılandırmalar AssemblyReferance sınıfından alınan derlemeye göre uygulanır.
        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyReferance).Assembly);

        // SaveChangesAsync, veritabanına yapılan değişiklikleri kaydeder.
        // Bu metot, Entity'nin eklenme veya güncellenme tarihlerinin otomatik olarak ayarlanmasını sağlar.
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Değişiklik izleyicisi (ChangeTracker) ile tüm BaseEntity türündeki entity'leri alır.
            var entities = ChangeTracker.Entries<BaseEntity>();

            // Her bir entity üzerinde işlemler yapılır.
            foreach (var entry in entities)
            {
                // Eğer entity ekleniyorsa (EntityState.Added):
                if (entry.State == EntityState.Added)
                {
                    // CreatedDate ve UpdatedDate tarihlerinin güncellenmesi.
                    entry.Property(p => p.CreatedDate).CurrentValue = DateTime.UtcNow;
                    entry.Property(p => p.UpdatedDate).CurrentValue = DateTime.UtcNow;

                    // IsDeleted property'si başlangıçta false olarak ayarlanır.
                    entry.Property(p => p.IsDeleted).CurrentValue = false;
                }
                // Eğer entity güncelleniyorsa (EntityState.Modified):
                else if (entry.State == EntityState.Modified)
                {
                    // Sadece UpdatedDate güncellenir.
                    entry.Property(p => p.UpdatedDate).CurrentValue = DateTime.UtcNow;
                }
            }
            // Veritabanına değişikliklerin kaydedilmesi işlemi yapılır.
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
