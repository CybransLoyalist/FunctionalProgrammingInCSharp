using System;

namespace FunctionalProgrammingInCSharp
{
    public static partial class FunctionalLibrary
    {
       public static Func<T, R2> Compose<T, R1, R2>(this Func<T, R1> input, Func<R1, R2> func)
        {
            return t => func(input(t));
        }
    }
}
