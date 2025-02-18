using AU_Framework.Domain.Abstract;

namespace AU_Framework.Domain.Entities;

public sealed class OrderDetail : BaseEntity
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }

    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal SubTotal { get; set; }

    public Order Order { get; set; }
    public Product Product { get; set; }
}

