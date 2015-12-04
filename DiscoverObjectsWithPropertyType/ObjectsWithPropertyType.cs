using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace DiscoverObjectsWithPropertyType
{
    public static class ObjectsWithPropertyType
    {
        public static IList<Type> Discover<T>()
        {
            var types = new List<Type>();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                types.AddRange(Discover<T>(assembly));

            return types;
        }

        public static IList<Type> Discover<T>(Assembly assembly)
        {
            return assembly.GetTypes()
                .Where(t => HasPropertyWithType<T>(t))
                .Select(t => t).ToList();
        }

        private static bool HasPropertyWithType<T>(Type type)
        {
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            return properties.Any(

                // find direct property type
                p => p.PropertyType == typeof(T) ||

                // find within generic type arguments
                // ICollection<MyHidingType>
                p.PropertyType.GenericTypeArguments.Any(a => a == typeof(T))
            );
        }
    }
}
