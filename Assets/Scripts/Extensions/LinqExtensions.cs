using System;
using System.Collections.Generic;

namespace Extensions
{
    public static class LinqExtensions
    {
        public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            using var enumerator = source.GetEnumerator();
            enumerator.MoveNext();
            (TSource source, TKey key) min = (enumerator.Current, keySelector(enumerator.Current));


            var comparer = Comparer<TKey>.Default;
            while (enumerator.MoveNext())
            {
                TKey temp = keySelector(enumerator.Current);
                if (comparer.Compare(temp, min.key) < 0)
                {
                    min = (enumerator.Current, temp);
                }
            }

            return min.source;
        }
    }
}