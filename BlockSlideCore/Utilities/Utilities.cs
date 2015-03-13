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

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rng)
        {
            var elements = source.ToArray();
            for (var i = elements.Length - 1; i > 0; i--)
            {
                var swapIndex = rng.Next(i + 1);
                var tmp = elements[i];
                elements[i] = elements[swapIndex];
                elements[swapIndex] = tmp;
            }
            return elements;
        }
    }
}
