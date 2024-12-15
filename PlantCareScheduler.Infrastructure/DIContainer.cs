using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PlantCareScheduler.Application.Data;
using PlantCareScheduler.Domain.Abstractions;
using PlantCareScheduler.Domain.Plants;
using PlantCareScheduler.Infrastructure.Data;
using PlantCareScheduler.Infrastructure.Repositories;

namespace PlantCareScheduler.Infrastructure;
public static class DIContainer
{
    public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddPersistence(services, configuration);

        return services;
    }

    private static void AddPersistence(IServiceCollection services, IConfiguration configuration)
    {
        var accountEndpoint = configuration["Cosmos:EndpointUri"] ?? throw new ArgumentNullException(nameof(configuration));
        var accountKey = configuration["Cosmos:PrimaryKey"] ?? throw new ArgumentNullException(nameof(configuration));
        var databaseName = "PlantCareDB";
        var containerName = "Plants";

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseCosmos(accountEndpoint: accountEndpoint, accountKey: accountKey, databaseName: databaseName);
        });

        services.AddScoped<IPlantRepository, PlantRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddSingleton<ICosmosDbConnectionFactory>(provider =>
        {
            return new CosmosDbConnectionFactory(accountEndpoint, accountKey, databaseName, containerName);
        });
    }

    public static void EnsureDatabaseCreated(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.EnsureCreated();
    }
}
