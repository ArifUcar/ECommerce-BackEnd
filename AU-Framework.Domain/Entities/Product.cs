

using AU_Framework.Domain.Abstract;

namespace AU_Framework.Domain.Entities;

public sealed class Product : BaseEntity
{
    public string ProductName { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public Guid CategoryId { get; set; }

    public Category Category { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; }
}


