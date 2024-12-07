using Nebx.API.BuildingBlocks.Extensions.Database;
using Nebx.API.BuildingBlocks.Shared.Contracts.DDD;

namespace Nebx.API.BuildingBlocks.Configurations.Interceptors;

public sealed class AuditableEntityInterceptor : SaveChangesInterceptor
{
    private readonly TimeProvider _timeProvider;

    public AuditableEntityInterceptor(TimeProvider timeProvider)
    {
        _timeProvider = timeProvider;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        if (eventData.Context is null) return base.SavingChanges(eventData, result);
        var entries = LoadAuditEntityEntries(eventData.Context);

        UpdateTimeAuditProperties(entries);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        if (eventData.Context is null) return base.SavingChangesAsync(eventData, result, cancellationToken);
        var entries = LoadAuditEntityEntries(eventData.Context);

        UpdateTimeAuditProperties(entries);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static List<EntityEntry<IEntity>> LoadAuditEntityEntries(DbContext context)
    {
        var entities = context.ChangeTracker
            .Entries<IEntity>()
            .ToList();

        return entities;
    }

    private void UpdateTimeAuditProperties(List<EntityEntry<IEntity>> entries)
    {
        entries.ForEach(x =>
        {
            var actionTime = _timeProvider.GetLocalNow().DateTime;

            if (x.State == EntityState.Added)
                x.Entity.CreatedAt = actionTime;

            if (x.State == EntityState.Added || x.State == EntityState.Modified || x.HasChangedOwnedEntities())
                x.Entity.ModifiedAt = actionTime;
        });
    }
}