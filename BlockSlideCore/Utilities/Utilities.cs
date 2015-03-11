using System;
using System.Collections.Generic;
using System.Linq;

namespace BlockSlideCore.Utilities
{
    public static class Utilities
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (action == null) throw new ArgumentNullException("action");
            foreach (var item in source)
            {
                action(item);
            }
        }

        public static void ForEachWithIndex<T>(this IList<T> source, Action<int, T> action)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (action == null) throw new ArgumentNullException("action");
            for (var index = 0; index < source.Count(); index++)
            {
                action(index, source.ElementAt(index));
            }
        }
    }
}
