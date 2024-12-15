using Microsoft.EntityFrameworkCore;
using PlantCareScheduler.Domain.Waterings;

namespace PlantCareScheduler.Infrastructure.Repositories;
internal sealed class WateringRepository : Repository<Watering>, IWateringRepository
{
    public WateringRepository(ApplicationDbContext dbContext) : base(dbContext)
    {

    }

    public async Task<Watering?> GetByPlantIdAndDateAsync(Guid plantId, DateOnly date, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<Watering>()
            .FirstOrDefaultAsync(
                x => x.PlantId == plantId && x.WateringDate == date,
                cancellationToken
            );
    }

}
