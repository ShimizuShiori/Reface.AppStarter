using System;
using System.Collections.Generic;

namespace Reface.AppStarter
{
    public static class DictionaryExtensions
    {
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
    }
}
