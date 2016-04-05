using System;
using System.Collections.Generic;

namespace Sogeti.Academy.Application
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> property)
        {
            var keys = new HashSet<TKey>();
            foreach (var item in source)
            {
                var key = property(item);
                if (keys.Contains(key))
                    continue;

                keys.Add(key);
                yield return item;
            }
        } 
    }
}
