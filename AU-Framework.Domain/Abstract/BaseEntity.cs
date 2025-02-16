namespace AU_Framework.Domain.Abstract;
 /// <summary>
 /// Burası base entity buranın amacı her entityde gerekli olan alanlar var bu alanları bu entityde belirtip diğer entiylerde tek tek yazmamıza gerek kalmıyor
 /// </summary>
public abstract class BaseEntity
{
    protected BaseEntity()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; } 
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedDate { get; set; }
    public bool IsDeleted { get; set; } = false;
}

