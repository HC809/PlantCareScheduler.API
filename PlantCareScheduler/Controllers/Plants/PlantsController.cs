using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlantCareScheduler.Application.Plants.GetPlants;
using PlantCareScheduler.Application.Plants.RegisterPlant;

namespace PlantCareScheduler.API.Controllers.Plants;

[Route("api/plants")]
[ApiController]
public class PlantsController : ControllerBase
{
    private readonly ISender _sender;

    public PlantsController(ISender sender)
    {
        _sender = sender;  
    }

    [HttpGet]
    public async Task<IActionResult> GetPlants(CancellationToken cancellationToken)
    {
        var query = new GetPlantsQuery();
        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> RegisterTenant(RegisterPlantRequest request, CancellationToken cancellationToken)
    {
        var command = new RegisterPlantCommand(
                request.Name,
                request.Type,
                request.WateringFrequencyDays,
                request.LastWateredDate,
                request.Location);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure) return BadRequest(result.Error);

        return Ok(result.Value);
    }
}
