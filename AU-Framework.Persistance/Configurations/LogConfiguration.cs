using AU_Framework.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AU_Framework.Persistance.Configurations;

public sealed class LogConfiguration : IEntityTypeConfiguration<Log>
{
    public void Configure(EntityTypeBuilder<Log> builder)
    {
        // Table name
        builder.ToTable("Logs");

        // Primary key
        builder.HasKey(x => x.Id);

        // Property configurations
        builder.Property(x => x.Level)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Message)
            .IsRequired()
            .HasMaxLength(4000);

        builder.Property(x => x.Exception)
            .HasMaxLength(8000);

        builder.Property(x => x.MethodName)
            .HasMaxLength(255);

        builder.Property(x => x.RequestPath)
            .HasMaxLength(2000);

        builder.Property(x => x.RequestMethod)
            .HasMaxLength(20);

        builder.Property(x => x.RequestBody)
            .HasMaxLength(4000);

        builder.Property(x => x.UserId)
            .HasMaxLength(450);

        builder.Property(x => x.UserName)
            .HasMaxLength(256);

        // Indexes
        builder.HasIndex(x => x.Level);
        builder.HasIndex(x => x.CreatedDate);
        builder.HasIndex(x => x.UserId);
    }
} 