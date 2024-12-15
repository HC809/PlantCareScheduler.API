
using Microsoft.Azure.Cosmos;

namespace PlantCareScheduler.Application.Data;
public interface ICosmosDbConnectionFactory
{
    Task<Container> CreateContainerAsync();
}
