using Microsoft.Azure.Cosmos;
using PlantCareScheduler.Application.Abstractions.Messaging;
using PlantCareScheduler.Application.Data;
using PlantCareScheduler.Domain.Abstractions;

namespace PlantCareScheduler.Application.Plants.GetPlants;
internal sealed class GetPlantsQueryHandler : IQueryHandler<GetPlantsQuery, IEnumerable<PlantResponse>>
{
    private readonly ICosmosDbConnectionFactory _cosmosDbConnectionFactory;

    public GetPlantsQueryHandler(ICosmosDbConnectionFactory cosmosDbConnectionFactory)
    {
        _cosmosDbConnectionFactory = cosmosDbConnectionFactory;
    }

    public async Task<Result<IEnumerable<PlantResponse>>> Handle(GetPlantsQuery request, CancellationToken cancellationToken)
    {
        var container = await _cosmosDbConnectionFactory.CreateContainerAsync();

        const string query = """
            SELECT
                p.id AS Id,
                p.name AS Name,
                p.type AS Type,
                p.wateringFrequencyDays AS WateringFrequencyDays,
                p.lastWateredDate AS LastWateredDate,
                p.location AS Location
            FROM c AS p
            """;

        var queryDefinition = new QueryDefinition(query);

        var iterator = container.GetItemQueryIterator<PlantResponse>(queryDefinition);

        var plants = new List<PlantResponse>();

        while (iterator.HasMoreResults)
        {
            var response = await iterator.ReadNextAsync();
            plants.AddRange(response.ToList());
        }

        return plants;
    }
}
