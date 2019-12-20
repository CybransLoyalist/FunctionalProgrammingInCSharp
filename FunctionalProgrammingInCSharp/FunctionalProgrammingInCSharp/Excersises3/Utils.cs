using System;
using System.Collections.Generic;
using System.Linq;
using static FunctionalProgrammingInCSharp.OptionCreators;

namespace FunctionalProgrammingInCSharp
{
    public partial class Utils
    {
        public static Option<T> ToEnum<T>(this string input) where T: struct
        {
            if (Enum.TryParse(input, out T result))
            {
                return Some(result);
            }
            return None;
        }

        public static Option<T> Lookup<T>(this IEnumerable<T> input, Func<T, bool> predicate)
        {
            return input.Where(predicate).Head();
        }

        public static Option<T> Head<T>(this IEnumerable<T> input)
        {
            return input.Any() ? Some(input.First()) : None;
        }
    }
}
