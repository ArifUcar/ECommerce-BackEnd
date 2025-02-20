using AU_Framework.Domain.Abstract;
using AU_Framework.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using AU_Framework.Persistance.Configurations;

namespace AU_Framework.Persistance.Context;

// AppDbContext, Entity Framework Core'dan DbContext sınıfını devralarak veritabanı ile etkileşim sağlar.
public sealed class AppDbContext : DbContext
{
    // DbContext sınıfının yapıcısı (constructor) içerideki seçenekleri (DbContextOptions) alır ve üst sınıfa gönderir.
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // DbSet tanımlamaları
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Log> Logs { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderStatus> OrderStatuses { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<ProductDetail> ProductDetails { get; set; }

    // OnModelCreating, model yapılandırmalarını uygulamak için kullanılır.
    // Burada, yapılandırmalar AssemblyReferance sınıfından alınan derlemeye göre uygulanır.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Entity configurations
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new LogConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new OrderStatusConfiguration());
        modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
        modelBuilder.ApplyConfiguration(new ProductDetailConfiguration());

        base.OnModelCreating(modelBuilder);
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
