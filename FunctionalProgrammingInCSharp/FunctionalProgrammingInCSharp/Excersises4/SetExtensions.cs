using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalProgrammingInCSharp.Excersises4
{
    public static class SetExtensions
    {
        public static ISet<R> Map<T,R>(this ISet<T> input, Func<T,R> func)
        {
            return input.Select(func).ToHashSet();
        }
    }
}
