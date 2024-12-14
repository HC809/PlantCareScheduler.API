namespace PlantCareScheduler.API.Controllers.Plants;

public sealed record RegisterPlantRequest(
    string Name,
    string Type,
    int WateringFrequencyDays,
    DateTime LastWateredDate,
    string Location);
