using PlantCareScheduler.Domain.Abstractions;

namespace PlantCareScheduler.Domain.Plants;
public sealed class Plant : Entity
{
    private Plant(
        Guid id,
        string name,
        PlantType type,
        int wateringFrequencyDays,
        DateTime lastWateredDate,
        DateTime nextWateringDate,
        string location) : base(id)
    {
        Id = id;
        Name = name;
        Type = type;
        WateringFrequencyDays = wateringFrequencyDays;
        LastWateredDate = lastWateredDate;
        NextWateringDate = nextWateringDate;
        Location = location;
    }

    public string Name { get; private set; }
    public PlantType Type { get; private set; } // 'succulent'| 'tropical'| 'herb'| 'cacti'
    public int WateringFrequencyDays { get; private set; }
    public DateTime LastWateredDate { get; private set; }
    public string Location { get; private set; }
    public DateTime NextWateringDate { get; private set; }

    public string GetStatus()
    {
        var today = DateTime.UtcNow.Date;

        if (NextWateringDate < today)
            return "Overdue";

        if ((NextWateringDate - today).TotalDays <= 2)
            return "Due Soon";

        return "OK";
    }


    public static Plant Create(string name, PlantType type, int wateringFrequencyDays, DateTime lastWateredDate, string location)
    {
        var plant = new Plant(Guid.CreateVersion7(), name, type, wateringFrequencyDays, lastWateredDate, lastWateredDate.AddDays(wateringFrequencyDays), location);

        return plant;
    }

    public void RegisterWatering()
    {
        LastWateredDate = DateTime.UtcNow;
        NextWateringDate = LastWateredDate.AddDays(WateringFrequencyDays);
    }

    // Empty constructor for EF Core
#nullable disable
    internal Plant() { }
#nullable restore
}
