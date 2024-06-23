using Common.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Common.Extensions
{
    public static class ListExtensions
    {
        public static List<T> Filter<T>(this List<T> list, Date start, Date end, string[] parameters, string searchText) where T : class
        {
            return list.Where(item =>
            (string.IsNullOrEmpty(searchText) ? true : CheckSerchTexts(typeof(T), parameters, item, searchText)) &&
               (((start != null) ? ((typeof(T).GetProperty(start.PropName).PropertyType.Equals(typeof(CustomDateBase)) &&
               typeof(T).GetProperty(start.PropName) != null)
               ? ((CustomDateBase)typeof(T).GetProperty(start.PropName).GetValue(item)).EnOrginalDate >= start.DateValue.EnOrginalDate : true) : true) &&
               ((end != null) ? ((typeof(T).GetProperty(end.PropName).PropertyType.Equals(typeof(CustomDateBase)) &&
               typeof(T).GetProperty(end.PropName) != null)
               ? ((CustomDateBase)typeof(T).GetProperty(end.PropName).GetValue(item)).EnOrginalDate < end.DateValue.EnOrginalDate : true) : true)))
                .ToList();
        }

        public static List<T> Random<T>(this List<T> list)
        {
            return (from r in list orderby Guid.NewGuid() ascending select r).ToList();
        }

        public static MoveList<T> Move<T>(this List<T> list, Expression<Func<T, bool>> selector)
        {
            var movedList = list.AsQueryable().Where(selector).ToList();
            return new MoveList<T>()
            {
                MovedList = movedList,
                OldList = list.Except(movedList).ToList()
            };
        }

        public static List<TResult> LeftOuterJoin<TOuter, TInner, TKey, TResult>(
      this List<TOuter> outer,
      List<TInner> inner,
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
                .SelectMany(x => x.b.DefaultIfEmpty(), (x, b) => resultSelector(x.a, b)).ToList();
        }

        #region Private

        public static bool CheckSerchTexts<T>(Type type, string[] parameters, T item, string searchText) where T : class
        {
            foreach (string member in parameters)
            {
                if (type.GetProperty(member) != null)
                {
                    if (type.GetProperty(member).GetValue(item).ToString().Contains(searchText))
                        return true;
                }
            }
            return false;
        }

        public class MoveList<T>
        {
            public List<T> MovedList { get; set; }

            public List<T> OldList { get; set; }
        }

        #endregion Private
    }
}