using System;
using static FunctionalProgrammingInCSharp.OptionCreators;
using Unit = System.ValueTuple;

namespace FunctionalProgrammingInCSharp
{
    public static class OptionExtensions
    {
        public static Option<R> Map<T,R>(this Option<T> option, Func<T,R> map)
        {
            return option.Match(
                () => None,
                (some) => Some(map(some)));
        }

        public static Option<Unit> ForEach<T>(this Option<T> option, Action<T> action)
        {
            return option.Map(action.ToFunc());
        }

        public static Option<R> Bind<T, R>(this Option<T> option, Func<T, Option<R>> bind)
        {
            return option.Match(
                () => None,
                (some) => bind(some));
        }

        public static Option<R> MapWithBind<T, R>(this Option<T> option, Func<T, R> map)
        {
            return option.Bind(t => Some(map(t)));
        }

        public static Option<T> Where<T>(this Option<T> input, Func<T, bool> predicate)
            => input.Match(
                () => None,
                (some) => predicate(some) ? input : None);
    }
}
