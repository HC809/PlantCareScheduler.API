using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using PlantCareScheduler.Application.Abstractions.Behaviors;

namespace PlantCareScheduler.API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var exceptionDetails = GetExceptionDetails(exception);

        _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

        await WriteProblemDetailsAsync(context, exceptionDetails);
    }

    private ExceptionDetails GetExceptionDetails(Exception exception)
    {
        return exception switch
        {
            ValidationException validationException => new ExceptionDetails(
                StatusCodes.Status422UnprocessableEntity,
                "ValidationFailure",
                "Validation Error",
                validationException.Message,
                validationException.Errors),

            CosmosException cosmosException => MapCosmosExceptionToExceptionDetails(cosmosException),

            DbUpdateException dbUpdateException when dbUpdateException.InnerException is CosmosException cosmosInnerException
                => MapCosmosExceptionToExceptionDetails(cosmosInnerException),

            _ => new ExceptionDetails(
                StatusCodes.Status500InternalServerError,
                "ServerError",
                "Server Error",
                exception.Message,
                null)
        }; ;
    }

    private ExceptionDetails MapCosmosExceptionToExceptionDetails(CosmosException cosmosException)
    {
        var statusCode = (int)cosmosException.StatusCode;

        return new ExceptionDetails(
            Status: statusCode,
            Type: "CosmosDbError",
            Title: cosmosException.Message,
            Detail: cosmosException.ResponseBody,
            Errors: null);
    }

    private static async Task WriteProblemDetailsAsync(HttpContext context, ExceptionDetails exceptionDetails)
    {
        var problemDetails = new ProblemDetails
        {
            Status = exceptionDetails.Status,
            Type = exceptionDetails.Type,
            Title = exceptionDetails.Title,
            Detail = exceptionDetails.Detail,
        };

        if (exceptionDetails.Errors is not null)
        {
            problemDetails.Extensions["errors"] = exceptionDetails.Errors;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = exceptionDetails.Status;
        await context.Response.WriteAsJsonAsync(problemDetails);
    }

    internal sealed record ExceptionDetails(
       int Status,
       string Type,
       string Title,
       string Detail,
       IEnumerable<object>? Errors);
}
