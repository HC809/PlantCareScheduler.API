namespace PlantCareScheduler.Application.Watering.WateringHistory;
public sealed class WateringHistoryResponse
{
    public string PlantName { get; set; } = string.Empty;
    public string PlantType { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public DateTime WateringDate { get; set; }
}

