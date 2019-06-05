using System;
using System.Collections.Generic;

namespace Reface.AppStarter
{
    public static class Ext
    {
        public static void ForEach<T>(this IEnumerable<T> list, Action<T> handler)
        {
            foreach (var item in list)
                handler(item);
        }
    }
}
