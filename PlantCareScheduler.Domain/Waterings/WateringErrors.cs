using PlantCareScheduler.Domain.Abstractions;

namespace PlantCareScheduler.Domain.Waterings;
public static class WateringErrors
{
    public static readonly Error AlreadyWateredToday = new Error("AlreadyWateredToday", "Plant has already been watered today.");
}
