using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctionalProgrammingInCSharp.Excersises4
{
    public static class DictionaryExtensions
    {
        public static IDictionary<K, R> Map<K,V, R>(this IDictionary<K, V> input, Func<V, R> func)
        {
            return input.ToDictionary(
                a => a.Key,
                a => func(a.Value));
        }
    }
}
