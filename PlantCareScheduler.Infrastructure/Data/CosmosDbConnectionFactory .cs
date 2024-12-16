using Microsoft.Azure.Cosmos;
using PlantCareScheduler.Application.Data;

namespace PlantCareScheduler.Infrastructure.Data;
internal sealed class CosmosDbConnectionFactory : ICosmosDbConnectionFactory
{
    private readonly CosmosClient _cosmosClient;
    private readonly string _databaseName;

    public CosmosDbConnectionFactory(string endpointUri, string primaryKey, string databaseName)
    {
        _cosmosClient = new CosmosClient(endpointUri, primaryKey);
        _databaseName = databaseName;
    }

    public async Task<Container> CreateContainerAsync(string containerName, string partitionKey)
    {
        var database = await _cosmosClient.CreateDatabaseIfNotExistsAsync(_databaseName);

        var container = await database.Database.CreateContainerIfNotExistsAsync(
            containerName,
            partitionKeyPath: partitionKey
        );

        return container.Container;
    }
}
