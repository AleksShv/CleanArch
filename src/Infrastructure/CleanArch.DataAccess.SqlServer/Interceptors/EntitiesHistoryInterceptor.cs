using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using CleanArch.Infrastructure.Contracts.UserProvider;
using CleanArch.DataAccess.SqlServer.Models;
using CleanArch.Entities.Base;

namespace CleanArch.DataAccess.SqlServer.Interceptors;

internal class EntitiesHistoryInterceptor : SaveChangesInterceptor
{
    private readonly ICurrentUserProvider _userProvider;

    public EntitiesHistoryInterceptor(ICurrentUserProvider userProvider)
    {
        _userProvider = userProvider;
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
        var histories = new List<EntityHistory>();

        var entries = context.ChangeTracker
            .Entries<IHistoricalEntity>();

        string user;
        try
        {
            user = _userProvider.GetUserId();
        }
        catch
        {
            user = "System";
        }

        foreach (var entry in entries)
        {
            if (entry.State is EntityState.Added)
            {
                var payload = entry.Properties
                    .ToDictionary(p => p.Metadata.Name, p => p.CurrentValue);

                AddHistoryToList(histories, entry, user, "Added", payload);
            }

            else if (entry.State is EntityState.Modified)
            {
                var payload = entry.Properties
                    .Where(p => p.IsModified)
                    .Where(p => p.CurrentValue != p.OriginalValue)
                    .ToDictionary(p => p.Metadata.Name, p => p.CurrentValue);

                AddHistoryToList(histories, entry, user, "Updated", payload);
            }

            else if (entry.State is EntityState.Deleted)
            {
                AddHistoryToList(histories, entry, user, "Deleted", null);
            }
        }

        return histories.ToArray();
    }

    private static void AddHistoryToList(List<EntityHistory> list, EntityEntry<IHistoricalEntity> entry, string user, string action, object? payload)
        => list.Add(new EntityHistory
        {
            EntityType = entry.Entity.ToString()!,
            CreatedAt = DateTimeOffset.UtcNow,
            Originator = user,
            Action = action,
            Payload = payload,
        });
}