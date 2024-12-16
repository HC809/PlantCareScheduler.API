using Microsoft.Azure.Cosmos;
using PlantCareScheduler.Application.Abstractions.Messaging;
using PlantCareScheduler.Application.Data;
using PlantCareScheduler.Application.Plants;
using PlantCareScheduler.Domain.Abstractions;

namespace PlantCareScheduler.Application.Watering.WateringHistory;
public sealed class GetWateringHistoryQueryHandler : IQueryHandler<GetWateringHistoryQuery, IEnumerable<WateringHistoryResponse>>
{
    private readonly ICosmosDbConnectionFactory _cosmosDbConnectionFactory;

    public GetWateringHistoryQueryHandler(ICosmosDbConnectionFactory cosmosDbConnectionFactory)
    {
        _cosmosDbConnectionFactory = cosmosDbConnectionFactory;
    }

    public async Task<Result<IEnumerable<WateringHistoryResponse>>> Handle(GetWateringHistoryQuery request, CancellationToken cancellationToken)
    {
        var wateringContainer = await _cosmosDbConnectionFactory.CreateContainerAsync("Watering", "/PlantId");

        const string wateringQuery = """
            SELECT 
                w.id AS Id, 
                w.PlantId AS PlantId, 
                w.WateringDate AS WateringDate
            FROM c AS w
            """;

        var wateringQueryDefinition = new QueryDefinition(wateringQuery);
        var wateringIterator = wateringContainer.GetItemQueryIterator<WateringResponse>(wateringQueryDefinition);

        var wateringHistories = new List<WateringHistoryResponse>();

        while (wateringIterator.HasMoreResults)
        {
            var wateringResults = await wateringIterator.ReadNextAsync();

            foreach (var watering in wateringResults)
            {
                var plantContainer = await _cosmosDbConnectionFactory.CreateContainerAsync("Plants", "/Type");

                var plantQuery = new QueryDefinition("""
                    SELECT 
                        p.id AS Id, 
                        p.Name AS Name, 
                        p.Type AS Type, 
                        p.WateringFrequencyDays AS WateringFrequencyDays, 
                        p.Location AS Location
                    FROM c AS p
                    WHERE p.id = @PlantId
                    """).WithParameter("@PlantId", watering.PlantId);

                var plantIterator = plantContainer.GetItemQueryIterator<PlantResponse>(plantQuery);

                while (plantIterator.HasMoreResults)
                {
                    var plantResults = await plantIterator.ReadNextAsync();
                    var plant = plantResults.FirstOrDefault();

                    if (plant != null)
                    {
                        wateringHistories.Add(new WateringHistoryResponse
                        {
                            PlantName = plant.Name,
                            PlantType = plant.Type,
                            Location = plant.Location,
                            WateringDate = watering.WateringDate
                        });
                    }
                }
            }
        }

        return wateringHistories;
    }
}
