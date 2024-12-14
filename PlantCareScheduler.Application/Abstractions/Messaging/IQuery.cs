using MediatR;
using PlantCareScheduler.Domain.Abstractions;

namespace PlantCareScheduler.Application.Abstractions.Messaging;
public interface IQuery<TResponse> : IRequest<Result<TResponse>> { }
