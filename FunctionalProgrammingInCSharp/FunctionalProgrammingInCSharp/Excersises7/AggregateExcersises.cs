using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalProgrammingInCSharp.Excersises7
{
    public static class AggregateExcersises
    {
        public static IEnumerable<R> MapWithAggregate<T, R>(this IEnumerable<T> elems, Func<T, R> mappingFunc)
        {
            return elems.Aggregate(new List<R>(), (acc, elem) => { acc.Add(mappingFunc(elem)); return acc; });
        }

        public static IEnumerable<T> WhereWithAggregate<T>(this IEnumerable<T> elems, Func<T, bool> predicate)
        {
            return elems.Aggregate(new List<T>(), (acc, elem) =>
            {
                if (predicate(elem))
                {
                    acc.Add(elem);
                }
                return acc;
            });
        }

        public static IEnumerable<T> BindWithAggregate<T>(this IEnumerable<T> elems, Func<T, IEnumerable<T>> func)
        {
            return elems.Aggregate(new List<T>(), (acc, elem) =>
            {
                foreach(var e in func(elem))
                {
                    acc.Add(e);
                }
                return acc;
            });
        }
    }
}
