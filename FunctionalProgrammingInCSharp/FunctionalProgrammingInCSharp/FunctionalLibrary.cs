using System;
using System.Collections.Generic;
using Unit = System.ValueTuple;

namespace FunctionalProgrammingInCSharp
{
    public static partial class FunctionalLibrary
    {
        public static R Using<T, R>(Func<T> createDisposable, Func<T, R> functionOnDisposable) where T : IDisposable
        {
            using (var t = createDisposable())
            {
                return functionOnDisposable(t);
            }
        }

        public static Func<Unit> ToFunc(this Action action)
        {
            return () => { action(); return new Unit(); };
        }

        public static Func<T, Unit> ToFunc<T>(this Action<T> action)
        {
            return (x) => { action(x); return new Unit(); };
        }
    }
}
