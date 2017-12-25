using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAL
{
    public static class Distinct
    {

        public static IEnumerable<TSource> DistinctsBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}