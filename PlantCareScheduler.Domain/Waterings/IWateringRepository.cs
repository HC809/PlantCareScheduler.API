namespace PlantCareScheduler.Domain.Waterings;
public interface IWateringRepository
{
    Task<Watering?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    void Add(Watering tenant);
    Task<Watering?> GetByPlantIdAndDateAsync(Guid plantId, DateTime date, CancellationToken cancellationToken = default);
}
