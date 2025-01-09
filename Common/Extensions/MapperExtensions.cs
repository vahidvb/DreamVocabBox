using System;
using System.Linq;
using System.Reflection;

namespace Common.Extensions
{
    public static class MapperExtensions
    {
        public static TDestination MapTo<TDestination>(this object source) where TDestination : new()
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            TDestination destination = new TDestination();
            PropertyInfo[] sourceProperties = source.GetType().GetProperties();
            PropertyInfo[] destinationProperties = typeof(TDestination).GetProperties();
            foreach (var sourceProperty in sourceProperties)
            {
                var destinationProperty = destinationProperties.FirstOrDefault(p => p.Name == sourceProperty.Name && p.PropertyType == sourceProperty.PropertyType);
                if (destinationProperty != null && destinationProperty.CanWrite)
                    destinationProperty.SetValue(destination, sourceProperty.GetValue(source));
            }

            return destination;
        }
    }
}
