﻿namespace Nebx.API.BuildingBlocks.Shared.Contracts.DDD;

public interface IDomainEvent : INotification
{
    Guid EventId => Guid.CreateVersion7();
    public DateTime OccurredOn => DateTime.Now;
    public string EventType => GetType().AssemblyQualifiedName!;
}