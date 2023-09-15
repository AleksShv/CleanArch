using System.Reflection;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;

using CleanArch.Entities.Base;
using CleanArch.Utils;

namespace CleanArch.UseCases.Common.Utils;

internal static class RelationalExtensions
{
    public static Task<int> ExecuteUpdateFromMapAsync<TEntity, TMap>(this IQueryable<TEntity> source, TMap mapObject)
        where TEntity : class
    {
        var param = Expression.Parameter(typeof(SetPropertyCalls<TEntity>), "setter");
        Expression body = param;

        var setterMethod = typeof(SetPropertyCalls<TEntity>)
            .GetMethods(BindingFlags.Public | BindingFlags.Instance)
            .Where(mi => mi.Name == nameof(SetPropertyCalls<TEntity>.SetProperty) && mi.IsGenericMethodDefinition)
            .Select(mi => new
            {
                Method = mi,
                Args = mi.GetGenericArguments(),
                Params = mi.GetParameters()
            })
            .Where(x => x.Params.Length == 2 && x.Args.Length == 1 && x.Params[1].ParameterType == x.Args[0])
            .Select(x => x.Method)
            .First();

        foreach (var prop in typeof(TMap).GetProperties(BindingFlags.Instance | BindingFlags.Public))
        {
            var entityProp = typeof(TEntity).GetProperty(prop.Name, BindingFlags.Public | BindingFlags.Instance);
            if (entityProp is null) continue;

            var entityParam = Expression.Parameter(typeof(TEntity), "e");
            var propertyExpression = Expression.Lambda(Expression.Property(entityParam, prop.Name), entityParam);

            var setValue = prop.GetValue(mapObject);
            var valueExpression = Expression.Constant(setValue);

            var genericMethod = setterMethod.MakeGenericMethod(prop.PropertyType);
            body = Expression.Call(body, genericMethod, propertyExpression, valueExpression);
        }

        var lambda = Expression.Lambda<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>>(body, param);

        return source.ExecuteUpdateAsync(lambda);
    }

    public static Task<int> ExecuteSoftDeleteAsync<TEntity>(this IQueryable<TEntity> source, DateTimeOffset deletedAt, string deletedBy, CancellationToken cancellationToken = default)
        where TEntity : ISoftDeletableEntity
        => source
            .ExecuteUpdateAsync(e => e
                .SetProperty(p => p.IsDeleted, true)
                .SetProperty(p => p.DeletedAt, deletedAt)
                .SetProperty(x => x.DeletedBy, deletedBy),
                cancellationToken);

    public static Task<int> ExecuteAuditableUpdateAsync<TEntity>(
        this IQueryable<TEntity> source,
        Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setPropertyCall,
        DateTimeOffset lastModifiedAt,
        string lastModifiedBy,
        CancellationToken cancellationToken = default)
        where TEntity : class, IAuditableEntity
    {
        Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setAuditablePropertiesCall =
            auditableSetter => auditableSetter
                .SetProperty(u => u.LastModifiedAt, lastModifiedAt)
                .SetProperty(u => u.LastModifiedBy, lastModifiedBy);

        return source.ExecuteUpdateAsync(setPropertyCall.Merge(setAuditablePropertiesCall), cancellationToken);
    }

    public static Task<int> ExecuteAuditableUpdateFromMapAsync<TEntity, TMAp>(
        this IQueryable<TEntity> source,
        TMAp mapObject,
        DateTimeOffset lastModifiedAt,
        string lastModifiedBy,
        CancellationToken cancellationToken = default)
        where TEntity : class, IAuditableEntity
    {
        throw new NotImplementedException();
    }
}
