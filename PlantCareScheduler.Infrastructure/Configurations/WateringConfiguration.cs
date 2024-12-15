using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlantCareScheduler.Domain.Waterings;

namespace PlantCareScheduler.Infrastructure.Configurations;
public class WateringConfiguration : IEntityTypeConfiguration<Watering>
{
    public void Configure(EntityTypeBuilder<Watering> builder)
    {
        builder.ToContainer("Waterings");   
        builder.HasKey(w => w.Id);
        builder.HasPartitionKey(w => w.PlantId);
        builder.Property(w => w.PlantId).IsRequired();
    }
}
