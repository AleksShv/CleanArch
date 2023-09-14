using System.Linq.Expressions;

using AutoMapper;
using AutoMapper.Configuration;

namespace CleanArch.UseCases.Common.Utils;

internal static class MapperExtensions
{
    public static IMappingExpression<TSource, TDest> ForRecordParam<TSource, TDest, TMember>(
        this IMappingExpression<TSource, TDest> mappingExpression, 
        Expression<Func<TDest, TMember>> member,
        Action<ICtorParamConfigurationExpression<TSource>> memberOptions)
    {
        var memberName = ((MemberExpression)member.Body).Member.Name;
        return mappingExpression.ForCtorParam(memberName, memberOptions);
    }
}
