using FluentValidation;
using Microsoft.EntityFrameworkCore;

using CleanArch.Entities.Base;

namespace CleanArch.UseCases.Common.Utils;

internal static class ValidatorExtensions
{
    public static IRuleBuilderOptions<T, TProperty> ContainsIn<T, TEntity, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, DbSet<TEntity> set)
        where TEntity : Entity<TProperty>
        where TProperty : IEquatable<TProperty>
    {
        return ruleBuilder.MustAsync(set.ContainsWithIdAsync);
    }
}
