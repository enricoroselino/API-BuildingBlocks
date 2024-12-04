using MediatR;

namespace Nebx.API.BuildingBlocks.Shared.Contracts.CQRS;

public interface IQuery<out TResponse> : IRequest<TResponse> where TResponse : notnull
{
}