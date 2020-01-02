using System;

namespace FunctionalProgrammingInCSharp.Excersises6
{
    public static class FuncExtensions
    {
        public static Exceptional<T> TryRun<T>(this Func<T> func)
        {
            try
            {
                return func();
            }
            catch(Exception ex)
            {
                return ex;
            }
        }

        public static Either<L, T> RunSafely<T, L>(this Func<T> func, Func<Exception, L> toLeft)
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                return toLeft(ex);
            }
        }

        public static Func<T2, R> Apply<T1, T2, R>(this Func<T1, T2, R> func, T1 t1)
        {
            return t2 => func(t1, t2);
        }

        public static Func<T2, T3, R> Apply<T1, T2, T3, R>(this Func<T1, T2, T3, R> func, T1 t1)
        {
            return ( t2, t3) => func(t1, t2, t3);
        }

        public static Func<T1, R> ApplyR<T1, T2, R>(this Func<T1, T2, R> func, T2 t2)
        {
            return t1 => func(t1, t2);
        }

        public static Func<T1, Func<T2, R>> Curry<T1, T2, R>(this Func<T1, T2, R> func)
        {
            return t1 => t2 => func(t1, t2);
        }
    }
}
