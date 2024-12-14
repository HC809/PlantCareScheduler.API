using Microsoft.EntityFrameworkCore;
using PlantCareScheduler.Domain.Abstractions;

namespace PlantCareScheduler.Infrastructure.Repositories;
internal abstract class Repository<T> where T : Entity
{
    protected readonly ApplicationDbContext _dbContext;

    protected Repository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);


    public void Add(T entity) => _dbContext.Add(entity);
}
