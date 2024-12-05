using Microsoft.EntityFrameworkCore.Diagnostics;
using Nebx.API.BuildingBlocks.Extensions;
using Nebx.API.BuildingBlocks.Shared.Contracts.DDD;

namespace Nebx.API.BuildingBlocks.Configurations.Interceptors;

internal sealed class TimeAuditEntityInterceptor : SaveChangesInterceptor
{
    private readonly TimeProvider _timeProvider;

    public TimeAuditEntityInterceptor(TimeProvider timeProvider)
    {
        _timeProvider = timeProvider;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateAuditableProperties(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        UpdateAuditableProperties(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateAuditableProperties(DbContext? context)
    {
        if (context is null) return;

        var entities = context.ChangeTracker
            .Entries<IEntity>()
            .ToList();

        entities.ForEach(x =>
        {
            var actionTime = _timeProvider.GetLocalNow().DateTime;

            if (x.State == EntityState.Added)
                x.Entity.CreatedAt = actionTime;

            if (x.State == EntityState.Added || x.State == EntityState.Modified || x.HasChangedOwnedEntities())
                x.Entity.ModifiedAt = actionTime;
        });
    }
}