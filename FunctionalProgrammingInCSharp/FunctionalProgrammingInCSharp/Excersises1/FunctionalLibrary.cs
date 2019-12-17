using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctionalProgrammingInCSharp
{
    public static partial class FunctionalLibrary
    {
        public static Func<T,bool> Negate<T>(this Func<T,bool> predicate)
        {
            return x => !predicate(x);
        }

        public static IEnumerable<T> SortWithCopy<T>(this IEnumerable<T> input)
        {
            var copy = input.ToList();
            copy.Sort();
            return copy;
        }

        public static IEnumerable<T> SortWithCopy<T>(this IEnumerable<T> input, Comparison<T> comparison)
        {
            var copy = input.ToList();
            copy.Sort(comparison);
            return copy;
        }
    }
}
