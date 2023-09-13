using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace CleanArch.UseCases.Common.Utils;

internal static class ExpressionExtensions
{
    public static Expression<Func<TIn,TOut>> Merge<TIn, TInOut, TOut>(this Expression<Func<TIn, TInOut>> inExp, Expression<Func<TInOut, TOut>> outExp)
    {
        return new ExpressionMerger().Merge(inExp, outExp);
    }


    private class ExpressionMerger : ExpressionVisitor
    {
        private Expression? _from;
        private Expression? _to;

        public Expression<Func<TIn, TOut>> Merge<TIn, TInOut, TOut>(
            Expression<Func<TIn, TInOut>> input,
            Expression<Func<TInOut, TOut>> output)
        {
            (_from, _to) = (output.Parameters.Single(), input.Body);
            var body = Visit(output.Body);
            return Expression.Lambda<Func<TIn, TOut>>(body, input.Parameters.Single());
        }

        [return: NotNullIfNotNull("node")]
        public override Expression? Visit(Expression? node)
        {
            return node == _from ? _to : base.Visit(node);
        }
    }
}
