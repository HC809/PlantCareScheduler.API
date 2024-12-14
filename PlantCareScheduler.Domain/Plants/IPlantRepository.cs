namespace PlantCareScheduler.Domain.Plants;
public interface IPlantRepository
{
    Task<Plant?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    void Add(Plant tenant);
}
