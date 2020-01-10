using System;

namespace FunctionalProgrammingInCSharp
{
    public static class ExceptionalExtensions
    {
        public static Exceptional<R> Apply<T,R>(this Exceptional<Func<T,R>> exceptionalOfFunc, Exceptional<T> exceptionalOfT)
        {
            return exceptionalOfFunc.Match(
                ex => ex,
                func => exceptionalOfT.Match(
                    vEx => vEx,
                    t => new Exceptional<R>(func(t))
                ));
        }
    }
}
