using AU_Framework.Domain.Abstract;

namespace AU_Framework.Domain.Entities;

public sealed   class OrderStatus : BaseEntity
{
    public string StatusName { get; set; }

    public ICollection<Order> Orders { get; set; }
}

