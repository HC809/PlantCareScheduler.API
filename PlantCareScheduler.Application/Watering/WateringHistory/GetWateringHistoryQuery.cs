using PlantCareScheduler.Application.Abstractions.Messaging;

namespace PlantCareScheduler.Application.Watering.WateringHistory;
public sealed record GetWateringHistoryQuery : IQuery<IEnumerable<WateringHistoryResponse>>;

