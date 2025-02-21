using AU_Framework.Domain.Abstract;

namespace AU_Framework.Domain.Entities;

public sealed class Order : BaseEntity
{
    public Guid UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public Guid OrderStatusId { get; set; }  // Lookup table reference

    // Sipariş veren kişi bilgileri
    public string CustomerName { get; set; }
    public string CustomerPhone { get; set; }

    // Adres bilgileri
    public string ShippingAddress { get; set; }
    public string City { get; set; }
    public string District { get; set; }
    public string ZipCode { get; set; }

    public User User { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; }
}

