using Common.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Extensions
{
    public static class IEnumerableExtentions
    {
        public static IEnumerable<TResult> LeftOuterJoin<TOuter, TInner, TKey, TResult>(
       this IEnumerable<TOuter> outer,
       IEnumerable<TInner> inner,
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

        public static GetPageResponse<T> GetPage<T>(this IEnumerable<T> list, int PageSize, int PageNumber) where T : class
        {
            return new GetPageResponse<T>
            {
                Count = list.Count(),
                List = list.Skip(PageSize * (PageNumber - 1)).Take(PageSize).ToList(),
                CurrentPage = PageNumber,
                IsLastPage = CalculateIsLastPage(list.Count(), PageNumber, PageSize)
            };
        }

        public static bool CalculateIsLastPage(int count, int pageNumber, int pageSize)
        {
            if (count % pageSize == 0)
                return count / pageSize == pageNumber ? true : false;
            else
                return pageNumber > count / pageSize ? true : false;
        }

        public static List<T> Random<T>(this IEnumerable<T> IEnumerable)
        {
            return (from r in IEnumerable orderby Guid.NewGuid() ascending select r).ToList();
        }
    }
}