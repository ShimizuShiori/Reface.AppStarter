using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Reface.AppStarter
{
    public static class Ext
    {
        #region IEnumerable

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> list, Action<T> handler)
        {
            foreach (var item in list)
                handler(item);
            return list;
        }

        #endregion

        #region Dictionary

        public static TValue GetOrCreate<TValue>(this Dictionary<string, object> target, string key, Func<string, TValue> factory)
        {
            object objResult;
            if (target.TryGetValue(key, out objResult))
                return (TValue)objResult;
            TValue result;
            result = factory(key);
            target[key] = result;
            return result;
        }

        #endregion

        #region Reflection

        public static string GetDescription(this MemberInfo memberInfo)
        {
            var attr = memberInfo.GetCustomAttribute<DescriptionAttribute>();
            return attr != null ? attr.Description : memberInfo.Name;
        }

        #endregion

    }
}
