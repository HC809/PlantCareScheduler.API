using PlantCareScheduler.Application.Abstractions.Exceptions;

namespace PlantCareScheduler.Application.Abstractions.Behaviors;
public sealed class ValidationException : Exception
{
    public ValidationException(IEnumerable<ValidationError> errors)
    {
        Errors = errors;
    }

    public IEnumerable<ValidationError> Errors { get; }
}
