namespace PlantCareScheduler.Application.Plants;
public sealed class PlantResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Type { get; init; } = string.Empty;
    public int WateringFrequencyDays { get; init; } 
    public DateTime LastWateredDate { get; init; }
    public string Location { get; init; } = string.Empty;
    public DateTime NextWateringDate { get; init; }

    public string Status { get; set; } = default!;
    public int DaysUntilNextWatering { get; set; }

}
