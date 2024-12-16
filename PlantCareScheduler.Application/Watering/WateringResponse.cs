namespace PlantCareScheduler.Application.Watering;
public sealed class WateringResponse
{
    public Guid Id { get; init; }
    public Guid PlantId { get; init; }
    public DateTime WateringDate { get; init; }
}
