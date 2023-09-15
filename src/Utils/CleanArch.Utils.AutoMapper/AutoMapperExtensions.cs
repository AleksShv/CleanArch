using System.Linq.Expressions;

using AutoMapper;
using AutoMapper.Configuration;

namespace CleanArch.Utils.AutoMapper;

public static class AutoMapperExtensions
{
    public static IMappingExpression<TSource, TDest> ForRecordParam<TSource, TDest, TMember>(
        this IMappingExpression<TSource, TDest> mappingExpression,
        Expression<Func<TDest, TMember>> param,
        Action<ICtorParamConfigurationExpression<TSource>> paramOptions)
    {
        var memberName = ((MemberExpression)param.Body).Member.Name;
        return mappingExpression
            .ForCtorParam(memberName, paramOptions);
    }
}
