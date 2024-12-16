using Microsoft.EntityFrameworkCore;
using PlantCareScheduler.Domain.Waterings;

namespace PlantCareScheduler.Infrastructure.Repositories;
internal sealed class WateringRepository : Repository<Watering>, IWateringRepository
{
    public WateringRepository(ApplicationDbContext dbContext) : base(dbContext)
    {

    }

    public async Task<Watering?> GetByPlantIdAndDateAsync(Guid plantId, DateTime date, CancellationToken cancellationToken = default)
    {
        var startOfDay = date.Date; 
        var endOfDay = date.Date.AddDays(1).AddTicks(-1); 

        return await _dbContext.Set<Watering>()
            .FirstOrDefaultAsync(
                x => x.PlantId == plantId &&
                     x.WateringDate >= startOfDay &&
                     x.WateringDate <= endOfDay,
                cancellationToken
            );
    }

}
