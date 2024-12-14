using Microsoft.EntityFrameworkCore;
using PlantCareScheduler.Domain.Abstractions;

namespace PlantCareScheduler.Infrastructure;
public sealed class ApplicationDbContext : DbContext, IUnitOfWork
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
