using PlantCareScheduler.Application.Abstractions.Messaging;
using PlantCareScheduler.Domain.Abstractions;
using PlantCareScheduler.Domain.Plants;

namespace PlantCareScheduler.Application.Plants.RegisterPlant;

internal sealed class RegisterPlantCommandHandler : ICommandHandler<RegisterPlantCommand, Guid>
{
    private readonly IPlantRepository _plantRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterPlantCommandHandler(IPlantRepository plantRepository, IUnitOfWork unitOfWork)
    {
        _plantRepository = plantRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(RegisterPlantCommand request, CancellationToken cancellationToken)
    {
        if (!Enum.TryParse<PlantType>(request.Type, true, out var plantType))
            return Result.Failure<Guid>(PlantErrors.InvalidPlantType);

        var plant = Plant.Create(
                request.Name,
                plantType,
                request.WateringFrequencyDays,
                request.LastWateredDate,
                request.Location
            );

        _plantRepository.Add(plant);
        await _unitOfWork.SaveChangesAsync();

        return plant.Id;
    }
}
