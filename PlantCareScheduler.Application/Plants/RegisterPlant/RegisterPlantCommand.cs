using PlantCareScheduler.Application.Abstractions.Messaging;

namespace PlantCareScheduler.Application.Plants.RegisterPlant;
public record RegisterPlantCommand(
    string Name,
    string Type,
    int WateringFrequencyDays,
    DateTime LastWateredDate,
    string Location) : ICommand<Guid>;
