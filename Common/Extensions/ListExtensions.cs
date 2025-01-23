using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Extensions
{
    public static class ListExtensions
    {
        public static bool HaveData(this List<object> expressions) => expressions != null && expressions.Any();
        public static string ToCommaSeperated(this List<object> expressions)
        {
            return string.Join(",", expressions);
        }
        public static List<T> ToList<T>(this string expression)
        {
            if (typeof(T) == typeof(int))
                return expression.Split(',').Select(x => (T)Convert.ChangeType(x.ToInt(), typeof(T))).ToList();

            if (typeof(T) == typeof(string))
                return expression.Split(',').Select(x => (T)Convert.ChangeType(x, typeof(T))).ToList();

            throw new InvalidOperationException($"Conversion to List<{typeof(T).Name}> is not supported.");
        }

    }
}