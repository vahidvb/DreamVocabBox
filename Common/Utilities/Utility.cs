using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Common.Utilities
{
    public static class Utility
    {
        public static bool ValueIsDefult(object obj)
        {
            if (obj == null)
                return true;
            if (obj.Equals(default(int)) || obj.Equals(default(Guid)) || obj.Equals(default(DateTime)) || obj.Equals(default(bool)))
                return true;
            return false;
        }

        public static Expression<Func<TModel, bool>> AddExpression<TModel, TType>(Expression<Func<TModel, bool>> expression, string propName, TType propValue)
            where TModel : class
        {
            var complied = expression.Compile();
            return item => complied(item) && typeof(TModel).GetProperty(propName).GetValue(item).Equals(propValue);
        }

        public static object RuntimeGenericMethod<TClass>(TClass methodClass, string methodName, Type genericType)
            where TClass : class
        {
            return methodClass.GetType().GetMethod(methodName).MakeGenericMethod(genericType).Invoke(methodClass, null);
        }

        public static Expression<Func<TResult, bool>> CreateCondition<TResult>(dynamic model)
        where TResult : class
        {
            Expression predicate = Expression.Constant(true);
            var pv = Expression.Parameter(typeof(TResult), "data");

            foreach (PropertyInfo property in model.GetType().GetProperties())
            {
                if (!ValueIsDefult(property.GetValue(model)))
                {
                    var newPredicate = Expression.Equal(
                        Expression.Property(pv, property.Name),
                        Expression.Constant(property.GetValue(model)));
                    predicate = Expression.AndAlso(predicate, newPredicate);
                }
            }

            return Expression.Lambda<Func<TResult, bool>>(predicate, pv);
        }

        public static Expression<Func<TResult, bool>> HistoryCondition<TInput, TResult>(TInput model, string startDateFilterName, string endDateFilterName)
    where TResult : class
        {
            Expression predicate = Expression.Constant(true);
            var pv = Expression.Parameter(typeof(TResult), "data");

            foreach (PropertyInfo property in typeof(TInput).GetProperties())
            {
                if (!ValueIsDefult(property.GetValue(model)))
                {
                    BinaryExpression newPredicate = null;
                    if (property.Name == "StartDate" && startDateFilterName != null)
                    {
                        newPredicate = Expression.GreaterThanOrEqual(
                            Expression.Property(pv, startDateFilterName),
                            Expression.Constant(property.GetValue(model)));
                    }
                    else if (property.Name == "EndDate" && endDateFilterName != null)
                    {
                        newPredicate = Expression.LessThan(
                            Expression.Property(pv, endDateFilterName),
                            Expression.Constant(property.GetValue(model)));
                    }
                    else
                    {
                        newPredicate = Expression.Equal(
                          Expression.Property(pv, property.Name),
                          Expression.Constant(property.GetValue(model)));
                    }
                    predicate = Expression.AndAlso(predicate, newPredicate);
                }
            }

            return Expression.Lambda<Func<TResult, bool>>(predicate, pv);
        }

        public static object ConvertTypeToModel<TModel>(string name, object value)
        {
            Type type = typeof(TModel).GetProperty(name).PropertyType;

            if (!type.Equals(value.GetType()))
            {
                if (type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                    type = Nullable.GetUnderlyingType(type);

                return Convert.ChangeType(value, type);
            }

            return value;
        }

        public static bool IsList(object obj)
        {
            if (obj == null) return false;
            return obj is IList &&
                   obj.GetType().IsGenericType &&
                   obj.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>));
        }

        public static int ConvertToPercent(int Count, int maxCount)
        {
            return (Count == 0 || maxCount == 0) ? 0 : (Count * 100) / maxCount;
        }

        public static void SaveObjectData<TData>(TData data, object thisObject) where TData : class
        {
            foreach (var prop in typeof(TData).GetProperties())
            {
                thisObject.GetType().GetProperty(prop.Name).SetValue(thisObject, prop.GetValue(data));
            }
        }

        public static object CreateGenericClass(Type classType, params object[] arguments)
        {
            Type constructedClass = classType.MakeGenericType(arguments.Select(item => item.GetType()).ToArray());
            return Activator.CreateInstance(constructedClass);
        }

        public static void AddProperty(ExpandoObject expando, string propertyName, object propertyValue)
        {
            var exDict = expando as IDictionary<string, object>;

            if (exDict.ContainsKey(propertyName))
                exDict[propertyName] = propertyValue;
            else
                exDict.Add(propertyName, propertyValue);
        }
    }
}