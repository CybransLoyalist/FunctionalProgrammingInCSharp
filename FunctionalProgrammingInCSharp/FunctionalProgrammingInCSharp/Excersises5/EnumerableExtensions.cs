using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctionalProgrammingInCSharp
{
    public static partial class EnumerableExtensions
    {
        public static IEnumerable<T> MyTake<T>(this IEnumerable<T> input, int count)
        {
            var i = 0;
            foreach(var elem in input)
            {
                if( i < count)
                {
                    yield return elem;
                    ++i;
                }
            }
        }

        public static IEnumerable<T> MyOrderBy<T>(this IEnumerable<T> input, Comparison<T> comparison)
        {
           return input.SortWithCopy(comparison);
        }

        public static double MyAverage(this IEnumerable<int> input)
        {
            return input.Sum() / input.Count();
        }
    }
}
