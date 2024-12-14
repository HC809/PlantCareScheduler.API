using Microsoft.EntityFrameworkCore;
using PlantCareScheduler.Domain.Plants;

namespace PlantCareScheduler.Infrastructure.Configurations;
public class PlantConfiguration : IEntityTypeConfiguration<Plant>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Plant> builder)
    {
        builder.ToContainer("Plants");
        builder.HasKey(p => p.Id);
        builder.HasPartitionKey(p => p.Type);

        builder.Property(p => p.Name).IsRequired();
        builder.Property(p => p.Type).HasConversion(
                v => v.ToString(),
                v => (PlantType)Enum.Parse(typeof(PlantType), v)
            ).IsRequired();
        builder.Property(p => p.WateringFrequencyDays).IsRequired();
        builder.Property(p => p.LastWateredDate).IsRequired();
        builder.Property(p => p.Location).IsRequired();
    }
}
