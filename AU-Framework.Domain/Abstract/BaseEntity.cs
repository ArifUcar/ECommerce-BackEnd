using System;

namespace AU_Framework.Domain.Abstract;
 /// <summary>
 /// Burası base entity buranın amacı her entityde gerekli olan alanlar var bu alanları bu entityde belirtip diğer entiylerde tek tek yazmamıza gerek kalmıyor
 /// </summary>
public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public bool IsDeleted { get; set; }

    public BaseEntity()
    {
        Id = Guid.NewGuid();
        CreatedDate = DateTime.Now;
        IsDeleted = false;
    }

    public DateTime? DeleteDate { get; set; }
}

