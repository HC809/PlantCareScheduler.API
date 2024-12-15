using PlantCareScheduler.Application.Abstractions.Messaging;
using PlantCareScheduler.Domain.Abstractions;
using PlantCareScheduler.Domain.Plants;
using PlantCareScheduler.Domain.Waterings;

namespace PlantCareScheduler.Application.Watering.RegisterWatering;
internal sealed class RegisterWateringCommandHandler : ICommandHandler<RegisterWateringCommand, Guid>
{
    private readonly IPlantRepository _plantRepository;
    private readonly IWateringRepository _wateringRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterWateringCommandHandler(
        IPlantRepository plantRepository,
        IWateringRepository wateringRepository,
        IUnitOfWork unitOfWork)
    {
        _plantRepository = plantRepository;
        _wateringRepository = wateringRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(RegisterWateringCommand request, CancellationToken cancellationToken)
    {
        var plant = await _plantRepository.GetByIdAsync(request.PlantId);
        if (plant == null)
            return Result.Failure<Guid>(PlantErrors.PlantNotFound);

        var existingWatering = await _wateringRepository.GetByPlantIdAndDateAsync(request.PlantId, DateOnly.FromDateTime(DateTime.UtcNow));
        if (existingWatering != null)
            return Result.Failure<Guid>(WateringErrors.AlreadyWateredToday);

        var watering = Domain.Waterings.Watering.Create(
            request.PlantId,
            DateTime.UtcNow
        );

        _wateringRepository.Add(watering);

        plant.RegisterWatering();
        _plantRepository.Update(plant);

        await _unitOfWork.SaveChangesAsync();

        return watering.Id;
    }
}
