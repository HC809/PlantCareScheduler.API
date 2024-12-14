namespace PlantCareScheduler.Domain.Abstractions;
public abstract class Entity
{
    protected Entity(Guid id) => Id = id;

    public Guid Id { get; init; }

    //public DateTime CreatedAt { get; protected set; }
    //public DateTime UpdatedAt { get; protected set; }
    //public bool IsDeleted { get; protected set; }
    //public DateTime? DeletedAt { get; protected set; }
    //public void Delete()
    //{
    //    IsDeleted = true;
    //    DeletedAt = DateTime.UtcNow;
    //}
    //public void Update()
    //{
    //    UpdatedAt = DateTime.UtcNow;
    //}
    //public void Restore()
    //{
    //    IsDeleted = false;
    //    DeletedAt = null;
    //}

    // Empty constructor for EF Core
    protected Entity() { }
}
