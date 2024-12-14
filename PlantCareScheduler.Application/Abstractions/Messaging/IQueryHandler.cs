using MediatR;
using PlantCareScheduler.Domain.Abstractions;

namespace PlantCareScheduler.Application.Abstractions.Messaging;
public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{ }
