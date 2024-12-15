using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlantCareScheduler.Application.Watering.RegisterWatering;

namespace PlantCareScheduler.API.Controllers.Watering;
[Route("api/watering")]
[ApiController]
public class WateringController : ControllerBase
{
    private readonly ISender _sender;
    public WateringController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterWatering(RegisterWateringRequest request, CancellationToken cancellationToken)
    {
        var command = new RegisterWateringCommand(request.PlantId);
        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure) return BadRequest(result.Error);

        return Ok(result.Value);
    }
}
