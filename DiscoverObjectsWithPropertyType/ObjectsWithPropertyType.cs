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

        public static IDictionary<Type, IEnumerable<string>> DiscoverWithMapping<T>(Assembly assembly)
        {
            return Discover<T>(assembly)
                .Select(t => new { Type = t, Properties = PropertiesWithType<T>(t) })
                .ToDictionary(x => x.Type, x => x.Properties);
        }

        private static IEnumerable<string> PropertiesWithType<T>(Type type)
        {
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            return properties
                .Where(QueryForType<T>(type))
                .Select(p => p.Name);
        }

        public static bool HasPropertyWithType<T>(Type type)
        {
            return PropertiesWithType<T>(type).Any();
        }

        private static Func<PropertyInfo, bool> QueryForType<T>(Type type)
        {
            return new Func<PropertyInfo,bool>(p =>
                // find direct property type
                p.PropertyType == typeof(T) ||

                // find within generic type arguments
                // ICollection<MyHidingType>
                p.PropertyType.GenericTypeArguments.Any(a => a == typeof(T))
            );
        }
    }
}
