using AU_Framework.Domain.Abstract;
using AU_Framework.Domain.Entities;
using Microsoft.EntityFrameworkCore;  

namespace AU_Framework.Persistance.Context
{
    // AppDbContext, Entity Framework Core'dan DbContext sınıfını devralarak veritabanı ile etkileşim sağlar.
    public sealed class AppDbContext : DbContext
    {
        // DbContext sınıfının yapıcısı (constructor) içerideki seçenekleri (DbContextOptions) alır ve üst sınıfa gönderir.
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Log> Logs { get; set; }

        // OnModelCreating, model yapılandırmalarını uygulamak için kullanılır.
        // Burada, yapılandırmalar AssemblyReferance sınıfından alınan derlemeye göre uygulanır.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User-Role ilişkisi
            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity(j => j.ToTable("UserRoles"));

            // Varsayılan rolleri ekle
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = Guid.NewGuid(), Name = "Admin", Description = "Sistem Yöneticisi" },
                new Role { Id = Guid.NewGuid(), Name = "Manager", Description = "Yönetici" },
                new Role { Id = Guid.NewGuid(), Name = "User", Description = "Kullanıcı" }
            );

            modelBuilder.Entity<Log>(entity =>
            {
                entity.ToTable("Logs");
                entity.Property(e => e.Level).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Message).IsRequired();
                entity.Property(e => e.Exception).HasMaxLength(4000);
                entity.Property(e => e.MethodName).HasMaxLength(255);
                entity.Property(e => e.RequestPath).HasMaxLength(2000);
                entity.Property(e => e.RequestMethod).HasMaxLength(20);
                entity.Property(e => e.RequestBody).HasMaxLength(4000);
                entity.Property(e => e.UserId).HasMaxLength(450);
                entity.Property(e => e.UserName).HasMaxLength(256);
            });
        }

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
