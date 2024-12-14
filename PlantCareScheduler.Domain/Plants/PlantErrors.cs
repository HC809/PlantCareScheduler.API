using PlantCareScheduler.Domain.Abstractions;

namespace PlantCareScheduler.Domain.Plants;
public static class PlantErrors
{
    public static readonly Error InvalidPlantType = new(
        "Plant.InvalidPlantType",
        "No existe un tipo de planta con el nombre especificado.");
}
