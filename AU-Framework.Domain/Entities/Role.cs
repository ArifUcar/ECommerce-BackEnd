using AU_Framework.Domain.Abstract;

namespace AU_Framework.Domain.Entities;

public sealed class Role : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<User> Users { get; set; }
} 