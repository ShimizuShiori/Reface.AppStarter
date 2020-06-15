using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Reface.AppStarter
{
    public static class TypeExtensions
    {
        /// <summary>
        /// 判断一个类型是否是或实现于 <see cref="IEnumerable"/>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsEnumerable(this Type type)
        {
            Type typeOfIEnumerable = typeof(IEnumerable);
            if (type == typeOfIEnumerable)
                return true;

            return typeOfIEnumerable.IsAssignableFrom(type);
        }

        /// <summary>
        /// 判断一个类型是否是或实现于 <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsEnumerableOfAny(this Type type)
        {
            Type typeOfIEnumerableT = typeof(IEnumerable<>);

            if (!type.IsGenericType)
                return false;

            if (type.GetGenericTypeDefinition() == typeOfIEnumerableT)
                return true;

            return type.GetInterface(typeOfIEnumerableT.FullName) != null;
        }

        /// <summary>
        /// 从一个类现了 <see cref="IEnumerable{T}"/> 的类型中分析成员类型。
        /// 当参数  <paramref name="type"/> 不是 或 未实现 <see cref="IEnumerable{T}"/> 时，返回 null
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type GetItemType(this Type type)
        {
            Type typeOfIEnumerableT = typeof(IEnumerable<>);

            if (!type.IsEnumerableOfAny()) return null;

            return type.GetInterface(typeOfIEnumerableT.FullName).GetGenericArguments()[0];
        }

        public static bool IsString(this Type type)
        {
            return type == typeof(string);
        }

        public static bool IsEnum(this Type type)
        {
            return type.IsEnum;
        }

        public static bool IsNumber(this Type type)
        {
            return type == typeof(byte)
                || type == typeof(int)
                || type == typeof(float)
                || type == typeof(long)
                || type == typeof(double)
                || type == typeof(short)
                || type == typeof(uint)
                || type == typeof(ulong)
                || type == typeof(ushort);
        }

        public static bool IsBoolean(Type type)
        {
            return type == typeof(bool);
        }

        public static T GetOrCreateAttribute<T>(this Type type, bool inherit, Func<T> creator)
            where T : Attribute
        {
            T attr = type.GetCustomAttribute<T>(inherit);
            if (attr == null) attr = creator();
            return attr;
        }
        public static T GetOrCreateAttribute<T>(this Type type, Func<T> creator)
            where T : Attribute
        {
            T attr = type.GetCustomAttribute<T>();
            if (attr == null) attr = creator();
            return attr;
        }
    }
}
