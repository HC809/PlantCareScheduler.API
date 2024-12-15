namespace PlantCareScheduler.Domain.Waterings;
public interface IWateringRepository
{
    Task<Watering?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    void Add(Watering tenant);
    Task<Watering?> GetByPlantIdAndDateAsync(Guid plantId, DateOnly date, CancellationToken cancellationToken = default);
}
