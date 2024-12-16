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
        var container = await _cosmosDbConnectionFactory.CreateContainerAsync("Plants", "/Type");

        const string query = """
            SELECT
                p.id AS Id,
                p.Name AS Name,
                p.Type AS Type,
                p.WateringFrequencyDays AS WateringFrequencyDays,
                p.LastWateredDate AS LastWateredDate,
                p.NextWateringDate AS NextWateringDate,
                p.Location AS Location
            FROM c AS p
            """;

        var queryDefinition = new QueryDefinition(query);

        var iterator = container.GetItemQueryIterator<PlantResponse>(queryDefinition);

        var plants = new List<PlantResponse>();

        while (iterator.HasMoreResults)
        {
            var response = await iterator.ReadNextAsync();
            foreach (var plant in response)
            {
                var today = DateTime.UtcNow.Date;
                var nextWateringDate = plant.NextWateringDate;
                var daysRemaining = (nextWateringDate - today).Days;

                string status = daysRemaining switch
                {
                    < 0 => "Overdue", 
                    <= 2 => "Due Soon", 
                    _ => "OK" 
                };

                plant.Status = status;
                plant.DaysUntilNextWatering = daysRemaining;

                plants.Add(plant);
            }
        }

        return plants;
    }
}
