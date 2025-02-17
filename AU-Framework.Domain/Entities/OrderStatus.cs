using AU_Framework.Domain.Abstract;

namespace AU_Framework.Domain.Entities;

public sealed class OrderStatus : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; } = true;
    public int DisplayOrder { get; set; }

    // Navigation property
    public ICollection<Order> Orders { get; set; }
}

