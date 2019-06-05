using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reface.AppStarter
{
    public static class Ext
    {
        public static TValue TryGetValue<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, Func<TValue> factory)
        {
            TValue result;
            if (dictionary.TryGetValue(key, out result)) return result;
            return factory();
        }

        public static TValue TryGetValue<TValue>(this Dictionary<string, object> dictionary, string key, Func<TValue> factory)
        {
            object result;
            if (dictionary.TryGetValue(key, out result)) return (TValue)result;
            return factory();
        }
    }
}
