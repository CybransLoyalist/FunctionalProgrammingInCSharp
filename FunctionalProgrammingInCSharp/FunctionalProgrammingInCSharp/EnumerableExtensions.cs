using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Immutable;

namespace FunctionalProgrammingInCSharp
{
    public static partial class EnumerableExtensions
    {
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

        public static IEnumerable<R> Map<T, R>(this IEnumerable<T> input, Func<T, R> func)
            => input.Select(func);

        public static IEnumerable<R> MapWithBind<T, R>(this IEnumerable<T> input, Func<T, R> func)
            => input.Bind(t => Return(func(t)));

        public static IEnumerable<R> Bind<T, R>(this IEnumerable<T> input, Func<T, IEnumerable<R>> func)
        {
            foreach(var elem in input)
            {
                foreach(var inner in func(elem))
                {
                    yield return inner;
                }
            }
        }

        public static IEnumerable<T> Return<T>(params T[] input)
            => input.ToImmutableList();
    }
}
