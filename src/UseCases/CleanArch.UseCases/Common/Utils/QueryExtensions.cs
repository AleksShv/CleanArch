using System.Linq.Expressions;
using System.Reflection;

using Microsoft.EntityFrameworkCore;

using CleanArch.Entities.Base;
using CleanArch.Utils;

namespace CleanArch.UseCases.Common.Utils;

internal static partial class QueryExtensions
{
    public static IQueryable<TEntity> Paging<TEntity>(this IQueryable<TEntity> source, int pageIndex, int pageSize)
        => source
            .Skip(pageIndex * pageSize)
            .Take(pageSize);

    public static IQueryable<TEntity> With<TEntity, TProperty>(this IQueryable<TEntity> source, Expression<Func<TEntity, TProperty>> propertyExpression, TProperty value)
        where TEntity : class
        => source.Where(
            Expression.Lambda<Func<TEntity, bool>>(
                Expression.Equal(
                    propertyExpression.Body, 
                    Expression.Constant(value)), 
                propertyExpression.Parameters));

    public static IQueryable<TEntity> WithId<TEntity, TId>(this IQueryable<TEntity> source, TId id)
        where TEntity : Entity<TId>
        where TId : IEquatable<TId>
        => source.Where(e => e.Id.Equals(id));

    public static IQueryable<TEntity> NotDeleted<TEntity>(this IQueryable<TEntity> source) 
        where TEntity : ISoftDeletableEntity
        => source.Where(e => !e.IsDeleted);

    public static Task<bool> ContainsWithAsync<TEntity, TProperty>(this IQueryable<TEntity> source, Expression<Func<TEntity, TProperty>> propertyExpression, TProperty value, CancellationToken cancellationToken = default)
        where TEntity : class
        => source.With(propertyExpression, value).AnyAsync(cancellationToken);

    public static Task<bool> ContainsWithIdAsync<TEntity, TId>(this IQueryable<TEntity> source, TId id, CancellationToken cancellationToken = default)
        where TEntity : Entity<TId>
        where TId : IEquatable<TId>
        => source.AnyAsync(e => e.Id.Equals(id), cancellationToken);

    public static Task<bool> ContainsWithIdAsync<TEntity, TId>(this IQueryable<TEntity> source, IEnumerable<TId> ids, CancellationToken cancellationToken = default)
        where TEntity : Entity<TId>
        where TId : IEquatable<TId>
        => source.AllAsync(e => ids.Contains(e.Id), cancellationToken);

    public static Task<TEntity?> FindByIdOrDefaultAsync<TEntity, TId>(this IQueryable<TEntity> source, TId id, CancellationToken cancellationToken = default)
        where TEntity : Entity<TId>
        where TId : IEquatable<TId>
        => source.FirstOrDefaultAsync(e => e.Id.Equals(id), cancellationToken);

    public static Task<TEntity> FindByIdAsync<TEntity, TId>(this IQueryable<TEntity> source, TId id, CancellationToken cancellationToken = default)
        where TEntity : Entity<TId>
        where TId : IEquatable<TId>
        => source.FirstAsync(e => e.Id.Equals(id), cancellationToken);

    public static IQueryable<TEntity> SearchWhen<TEntity>(this IQueryable<TEntity> source, bool condition, Expression<Func<TEntity, string>> members, string? searchString)
        => condition ? source.Search(members, searchString) : source;

    public static IQueryable<TEntity> Search<TEntity>(this IQueryable<TEntity> source, Expression<Func<TEntity, string>> members, string? searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
        {
            return source;
        }

        searchString = searchString.Replace(StringConsts.WhiteSpace, string.Empty).ToLower();

        var replaceMethod = typeof(string).GetMethod(nameof(string.Replace), new Type[] { typeof(string), typeof(string) })!;
        var containsMethod = typeof(string).GetMethod(nameof(string.Contains), new Type[] { typeof(string) })!;
        var toLowerMethod = typeof(string).GetMethods(BindingFlags.Instance | BindingFlags.Public)
            .Where(x => x.Name.Equals(nameof(string.ToLower)))
            .First();

        var lambda =
            Expression.Lambda<Func<TEntity, bool>>(
                Expression.Call(
                    Expression.Call(
                        Expression.Call(
                            instance: members.Body,
                            method: replaceMethod,
                            Expression.Constant(StringConsts.WhiteSpace), Expression.Constant(string.Empty)),
                        toLowerMethod),
                    containsMethod,
                    Expression.Constant(searchString)),
                members.Parameters);

        return source.Where(lambda);
    }

    public static IQueryable<TEntity> WhereWhen<TEntity>(this IQueryable<TEntity> source, bool condition, Expression<Func<TEntity, bool>> filter)
        => condition ? source.Where(filter) : source;
}
