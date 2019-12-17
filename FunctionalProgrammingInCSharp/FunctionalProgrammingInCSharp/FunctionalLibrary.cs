using System;
using System.Collections.Generic;

namespace FunctionalProgrammingInCSharp
{
    public static class Utils
    {
        public static string ToString<T>(this IEnumerable<T> enumerable) => string.Join(",", enumerable);
    }

    public static partial class FunctionalLibrary
    {
        public static R Using<T, R>(Func<T> createDisposable, Func<T, R> functionOnDisposable) where T : IDisposable
        {
            using (var t = createDisposable())
            {
                return functionOnDisposable(t);
            }
        }
    }
}
