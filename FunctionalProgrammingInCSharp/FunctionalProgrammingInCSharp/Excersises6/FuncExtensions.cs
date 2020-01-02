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
    }
}
