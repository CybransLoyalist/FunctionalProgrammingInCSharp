using System;

namespace FunctionalProgrammingInCSharp
{

    public static partial class FunctionalLibrary
    {
        public static Func<T,bool> Negate<T>(this Func<T,bool> predicate)
        {
            return x => !predicate(x);
        }
    }
}
