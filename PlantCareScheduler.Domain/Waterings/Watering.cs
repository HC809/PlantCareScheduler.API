using PlantCareScheduler.Domain.Abstractions;

namespace PlantCareScheduler.Domain.Waterings;
public sealed class Watering : Entity
{
    public Watering(Guid id, Guid plantId, DateTime wateringDate) : base(id)
    {
        Id = id;
        PlantId = plantId;
    }

    public Guid PlantId { get; private set; }
    public DateOnly WateringDate { get; private set; }

    public static Watering Create(Guid plantId, DateTime wateringDate)
    {
        var watering = new Watering(Guid.CreateVersion7(), plantId, wateringDate);

        return watering;
    }

#nullable disable
    internal Watering() { }
#nullable restore
}
