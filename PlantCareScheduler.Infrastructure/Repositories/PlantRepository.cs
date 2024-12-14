﻿using PlantCareScheduler.Domain.Plants;

namespace PlantCareScheduler.Infrastructure.Repositories;
internal sealed class PlantRepository : Repository<Plant>, IPlantRepository
{
    public PlantRepository(ApplicationDbContext dbContext) : base(dbContext)
    {

    }
}