using System;
using static FunctionalProgrammingInCSharp.OptionCreators;
using static FunctionalProgrammingInCSharp.EitherCreators;
using Unit = System.ValueTuple;
using FunctionalProgrammingInCSharp.Excersises6;

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

        public static Option<Func<T2, R>> Map<T1, T2, R>(this Option<T1> option, Func<T1, T2, R> map)
        {
            return option.Map(map.Curry());
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

        public static Option<R> Bind<T, R, L>(this Option<T> option, Func<T, Either<L, R>> bind)
        {
            return option.Match(
                () => None,
                (some) => bind(some).ToOption());
        }

        public static Option<R> MapWithBind<T, R>(this Option<T> option, Func<T, R> map)
        {
            return option.Bind(t => Some(map(t)));
        }

        public static Option<R> MapWithApply<T, R>(this Option<T> option, Func<T, R> map)
        {
            return Some(map).Apply(option);
        }

        public static Option<T> Where<T>(this Option<T> input, Func<T, bool> predicate)
            => input.Match(
                () => None,
                (some) => predicate(some) ? input : None);

        public static Either<L,T> ToEither<T,L>(this Option<T> input, L left)
        {
            return input.Match(
                () => (Either<L, T>)Left(left),
                s => Right(s));
        }
    }
}
