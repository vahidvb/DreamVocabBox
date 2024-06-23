using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Random<T>(this IQueryable<T> queryable)
        {
            return (from r in queryable orderby Guid.NewGuid() ascending select r);
        }

        public static IEnumerable<TResult> LeftOuterJoin<TOuter, TInner, TKey, TResult>(
        this IQueryable<TOuter> outer,
        IQueryable<TInner> inner,
        Func<TOuter, TKey> outerKeySelector,
        Func<TInner, TKey> innerKeySelector,
        Func<TOuter, TInner, TResult> resultSelector)
        {
            return outer
                .GroupJoin(inner, outerKeySelector, innerKeySelector, (a, b) => new
                {
                    a,
                    b
                })
                .SelectMany(x => x.b.DefaultIfEmpty(), (x, b) => resultSelector(x.a, b));
        }

        public static IEnumerable<TResult> RightOuterJoin<TOuter, TInner, TKey, TResult>(
       this IQueryable<TOuter> outer,
       IQueryable<TInner> inner,
       Func<TOuter, TKey> outerKeySelector,
       Func<TInner, TKey> innerKeySelector,
       Func<TOuter, TInner, TResult> resultSelector)
        {
            return outer
                .GroupJoin(inner, outerKeySelector, innerKeySelector, (a, b) => new
                {
                    a,
                    b
                })
                .SelectMany(x => x.b.DefaultIfEmpty(), (x, b) => resultSelector(x.a, b));
        }
    }
}
