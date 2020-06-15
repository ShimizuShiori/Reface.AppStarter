using System;
using System.Collections.Generic;
using System.Linq;

namespace Reface.AppStarter
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> list, Action<T> handler)
        {
            if (list == null) return list;
            foreach (var item in list)
                handler(item);
            return list;
        }

        public static string Join(this IEnumerable<string> list, string joiner)
        {
            if (joiner == null) joiner = "";
            if (list == null) return "";
            int count = list.Count();
            if (count == 0) return "";
            if (count == 1) return list.First();
            return list.Aggregate((a, b) => $"{a}{joiner}{b}");
        }
    }
}
