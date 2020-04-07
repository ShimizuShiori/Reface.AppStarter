using System;
using System.Collections.Generic;

namespace Reface.AppStarter.JsonSchema
{
    public static class JsonTypeHelper
    {
        public static bool IsString(Type type)
        {
            return type == typeof(string);
        }

        public static bool IsNumber(Type type)
        {
            return type == typeof(int)
                || type == typeof(float)
                || type == typeof(long)
                || type == typeof(double);
        }

        public static bool IsBoolean(Type type)
        {
            return type == typeof(bool);
        }

        public static bool IsArray(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                return true;
            return type.GetInterface(typeof(IEnumerable<>).FullName) != null;
        }

        public static bool IsObject(Type type)
        {
            return type.IsClass && !type.IsAbstract && !type.IsInterface;
        }
        public static Type GetArrayItemType(Type propertyType)
        {
            if (propertyType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                return propertyType.GetGenericArguments()[0];
            }
            var interfaceType = propertyType.GetInterface(typeof(IEnumerable<>).FullName);
            return interfaceType.GetGenericArguments()[0];
        }
    }
}
