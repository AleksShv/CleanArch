using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using CleanArch.Entities.Base;

namespace CleanArch.DataAccess.SqlServer.Utils;

internal static class MultiTenancyHelper
{    
    private const string PropertyName = "TenantId";
    private static readonly Expression PropertyNameExpression = Expression.Constant(PropertyName);

    public static void ConfigureTenantEntitiesWithShadowProperties<T>(ModelBuilder modelBuilder, T tenantId)
    {
        var tenantExpression = Expression.Constant(tenantId);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var paramExpression = Expression.Parameter(entityType.ClrType, "x");
            var tenantPropertyExpression = Expression.Call(
                typeof(EF), 
                nameof(EF.Property), 
                new[] { typeof(T) }, 
                paramExpression, PropertyNameExpression);
            var queryFilterExpression = Expression.Lambda(Expression.Equal(tenantPropertyExpression, tenantExpression), paramExpression);

            entityType.AddProperty(PropertyName, typeof(int));
            entityType.SetQueryFilter(queryFilterExpression);
        }
    }

    public static void ConfigureTenantEntities<T>(ModelBuilder modelBuilder, T tenantId)
    {
        var tenantExpression = Expression.Constant(tenantId);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(ITenantEntity).IsAssignableFrom(entityType.ClrType))
            {
                var paramExpression = Expression.Parameter(entityType.ClrType, "x");
                var tenantPropertyExpression = Expression.Property(paramExpression, nameof(ITenantEntity.TenantId));
                var queryFilterExpression = Expression.Lambda(Expression.Equal(tenantPropertyExpression, tenantExpression), paramExpression);

                entityType.SetQueryFilter(queryFilterExpression);
            }
            else
            {
                throw new InvalidOperationException($"Entity {entityType.ClrType.FullName} not assignable to {typeof(ITenantEntity).FullName} interface");
            }
        }
    }
}
