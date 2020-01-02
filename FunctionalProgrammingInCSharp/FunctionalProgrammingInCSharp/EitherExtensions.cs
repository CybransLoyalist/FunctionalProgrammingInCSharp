using System;
using static FunctionalProgrammingInCSharp.EitherCreators;
using static FunctionalProgrammingInCSharp.OptionCreators;
using Unit = System.ValueTuple;

namespace FunctionalProgrammingInCSharp
{
    public static class EitherExtensions
    {
        public static Either<L, RR> Map<L, R, RR>(this Either<L, R> either, Func<R, RR> func)
        {
            return either.Match<Either<L, RR>>(l => Left(l), r => Right(func(r)));
        }

        public static Either<LL, RR> UnbiasedMap<L, R, RR, LL>(this Either<L, R> either, Func<R, RR> funcRight, Func<L, LL> funcLeft)
        {
            return either.Match<Either<LL, RR>>(l => Left(funcLeft(l)), r => Right(funcRight(r)));
        }

        public static Either<L, RR> Bind<L, R, RR>(this Either<L, R> either, Func<R, Either<L, RR>> func)
        {
            return either.Match<Either<L, RR>>(l => Left(l), r => func(r));
        }

        public static Either<L, RR> Bind<L, R, RR>(this Either<L, R> either, Func<R, Option<RR>> func)
        {
            return either.Match<Either<L, RR>>(l => Left(l), r => func(r).ToEither(default(L)));
        }

        public static Either<L, Unit> ForEach<L, R>(this Either<L, R> either, Action<R> action)
        {
            return either.Map(action.ToFunc());
        }

        public static Option<R> ToOption<L,R>(this Either<L, R> either)
        {
            return either.Match(
                l => None,
                s => Some(s));
        }
    }
}
