using PlantCareScheduler.Domain.Abstractions;

namespace PlantCareScheduler.Domain.Plants;
public static class PlantErrors
{
    public static readonly Error InvalidPlantType = new(
        "Plant.InvalidPlantType",
        "There is not a type of plant with the specified name.");

    public static readonly Error PlantNotFound = new(
        "Plant.PlantNotFound",
        "Plant not found.");
}
