using PlantCareScheduler.Application.Abstractions.Messaging;

namespace PlantCareScheduler.Application.Plants.GetPlants;
public sealed record GetPlantsQuery() : IQuery<IEnumerable<PlantResponse>>;
