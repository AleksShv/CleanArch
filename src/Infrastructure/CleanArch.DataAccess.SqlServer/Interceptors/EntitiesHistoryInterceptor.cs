using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

using CleanArch.Infrastructure.Contracts.UserProvider;
using CleanArch.DataAccess.SqlServer.Models;
using CleanArch.Entities.Base;

using Changings = (string Action, System.Collections.Generic.Dictionary<string, object?>? Payload);

namespace CleanArch.DataAccess.SqlServer.Interceptors;

internal class EntitiesHistoryInterceptor : SaveChangesInterceptor
{
    private readonly IServiceProvider _serviceProvider;
    
    public EntitiesHistoryInterceptor(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        var context = eventData.Context;
        if (context is null)
        {
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        var histories = CreateHistories(context);
        if (histories.Any())
        {
            await context.AddRangeAsync(histories, cancellationToken);
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        var context = eventData.Context;
        if (context is null)
        {
            return base.SavingChanges(eventData, result);
        }

        var histories = CreateHistories(context);
        if (histories.Any())
        {
            context.AddRange(histories);
        }

        return base.SavingChanges(eventData, result);
    }

    private EntityHistory[] CreateHistories(DbContext context)
    {
        var entries = context.ChangeTracker
            .Entries<IHistoricalEntity>();

        if (!entries.Any())
        {
            return Array.Empty<EntityHistory>();
        }

        var histories = new List<EntityHistory>();

        string user;

        using (var scope = _serviceProvider.CreateScope())
        {
            try
            {
                user = scope.ServiceProvider
                    .GetRequiredService<ICurrentUserProvider>()
                    .GetUserId();
            }
            catch
            {
                user = "System";
            }
        }

        var now = DateTimeOffset.UtcNow;

        foreach (var entry in entries)
        {
            var entryEntityType = entry.Entity.GetType();
            var entityType = context.Model.FindEntityType(entryEntityType);

            var key = entityType
                ?.FindPrimaryKey()
                ?.Properties
                ?.Select(p => p.Name)
                ?.Single();

            var entityId = key is not null
                ? entry.Property(key).OriginalValue?.ToString()
                : null;

            if (entityType is null || string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(entityId))
            {
                throw new InvalidOperationException($"Entity {entryEntityType.Name} or its primary key not found");
            }

            Changings? changings = entry.State switch
            {
                EntityState.Added => 
                (
                    Enum.GetName(EntityState.Added)!, 
                    entry.Properties
                        .ToDictionary(p => p.Metadata.Name, p => p.CurrentValue)
                ),
                EntityState.Modified => 
                (
                    Enum.GetName(EntityState.Modified)!, 
                    entry.Properties
                        .Where(p => p.IsModified)
                        .Where(p => p.CurrentValue != p.OriginalValue)
                        .ToDictionary(p => p.Metadata.Name, p => p.CurrentValue)
                ),
                EntityState.Deleted => (
                    Enum.GetName(EntityState.Detached)!, 
                    null
                ),
                _ => null
            };

            if (changings.HasValue)
            {
                histories.Add(new()
                {
                    EntityType = entityType.ClrType.Name,
                    EntityId = entityId,
                    CreatedAt = now,
                    Originator = user,
                    Action = changings.Value.Action,
                    Payload = changings.Value.Payload
                });
            }
        }

        return histories.ToArray();
    }
}