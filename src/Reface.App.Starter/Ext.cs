using System;
using System.Collections.Generic;

namespace Reface.AppStarter
{
    public static class Ext
    {
        #region IEnumerable

        public static void ForEach<T>(this IEnumerable<T> list, Action<T> handler)
        {
            foreach (var item in list)
                handler(item);
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
    }
}
