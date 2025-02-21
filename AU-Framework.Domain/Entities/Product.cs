using AU_Framework.Domain.Abstract;

namespace AU_Framework.Domain.Entities;

public sealed class Product : BaseEntity
{
    public string ProductName { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public Guid CategoryId { get; set; }
    public string? ImagePath { get; set; }
    public string? Base64Image { get; set; }
    
    // İndirim özellikleri
    public decimal? DiscountedPrice { get; set; }
    public decimal? DiscountRate { get; set; }
    public DateTime? DiscountStartDate { get; set; }
    public DateTime? DiscountEndDate { get; set; }
    public bool IsDiscounted => 
        DiscountedPrice.HasValue && 
        DiscountStartDate.HasValue && 
        DiscountEndDate.HasValue && 
        DateTime.UtcNow >= DiscountStartDate && 
        DateTime.UtcNow <= DiscountEndDate;

    // Navigation properties
    public Category Category { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; }
    public ProductDetail ProductDetail { get; set; }
}


