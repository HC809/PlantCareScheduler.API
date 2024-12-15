using PlantCareScheduler.Application.Abstractions.Messaging;

namespace PlantCareScheduler.Application.Watering.RegisterWatering;
public record RegisterWateringCommand(
    Guid PlantId) : ICommand<Guid>;
