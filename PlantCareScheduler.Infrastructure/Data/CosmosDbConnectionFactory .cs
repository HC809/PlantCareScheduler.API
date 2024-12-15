using Microsoft.Azure.Cosmos;
using PlantCareScheduler.Application.Data;

namespace PlantCareScheduler.Infrastructure.Data;
internal sealed class CosmosDbConnectionFactory : ICosmosDbConnectionFactory
{
    private readonly CosmosClient _cosmosClient;
    private readonly string _databaseName;
    private readonly string _containerName;

    public CosmosDbConnectionFactory(string endpointUri, string primaryKey, string databaseName, string containerName)
    {
        _cosmosClient = new CosmosClient(endpointUri, primaryKey);
        _databaseName = databaseName;
        _containerName = containerName;
    }

    public async Task<Container> CreateContainerAsync()
    {
        var database = await _cosmosClient.CreateDatabaseIfNotExistsAsync(_databaseName);

        var container = await database.Database.CreateContainerIfNotExistsAsync(
            _containerName,
            partitionKeyPath: "/type" 
        );

        return container.Container;
    }
}
