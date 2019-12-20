using System.Collections.Generic;
using System.Linq;

namespace FunctionalProgrammingInCSharp
{
    public static partial class Utils
    {
        public static string PrependCounter(this IEnumerable<string> input)
        {
            return string.Join(" ", input
                .Zip(Enumerable.Range(1, input.Count()),
                (word, counter) => $"{counter}. {word}"));
        }

        public static string ToString<T>(this IEnumerable<T> enumerable) => string.Join(",", enumerable);
    }
}
