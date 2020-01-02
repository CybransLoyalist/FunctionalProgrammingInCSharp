using FunctionalProgrammingInCSharp.Option;
using System;
using System.Collections.Generic;
using static FunctionalProgrammingInCSharp.Either;

namespace FunctionalProgrammingInCSharp
{
    public static class EitherCreators
    {
        public static Either.Left<L> Left<L>(L l) => new Either.Left<L>(l);
        public static Either.Right<R> Right<R>(R r) => new Either.Right<R>(r);
    }

    public struct Either<L,R> : IEquatable<Either<L,R>>
    {
        private L _left;
        private R _right;
        public bool IsRight;

        private Either(L left)
        {
            _left = left;
            _right = default(R);
            IsRight = false;
        }
        private Either(R right)
        {
            _right = right;
            _left = default(L);
            IsRight = true;
        }

        public static implicit operator Either<L,R>(R r)
        {
            return new Either<L, R>(r);
        }

        public static implicit operator Either<L, R>(L l)
        {
            return new Either<L, R>(l);
        }

        public static implicit operator Either<L, R>(Either.Right<R> r)
        {
            return new Either<L, R>(r.Value);
        }

        public static implicit operator Either<L, R>(Either.Left<L> l)
        {
            return new Either<L, R>(l.Value);
        }

        public FinalR Match<FinalR>(Func<L, FinalR> onLeft, Func<R, FinalR> onRight)
        {
            return IsRight ? onRight(_right) : onLeft(_left);
        }

        public override bool Equals(object other)
        {
            return other is Either<L, R> otherEither && Equals(otherEither);
        }

        public bool Equals(Either<L, R> other)
        {
            return !IsRight && !other.IsRight ||
                _right.Equals(_right);
        }

        public bool Equals(Right<R> other)
        {
            return IsRight &&
                _right.Equals(other.Value);
        }

        public bool Equals(Left<L> other)
        {
            return !IsRight &&
                _left.Equals(other.Value);
        }
    }


    public static class Either
    {
        public struct Left<L>
        {
            internal L Value { get; }
            internal Left(L value) { Value = value; }

            public override string ToString() => $"Left({Value})";
        }

        public struct Right<R> 
        {
            internal R Value { get; }
            internal Right(R value) { Value = value; }

            public override string ToString() => $"Right({Value})";

            public Right<RR> Map<L, RR>(Func<R, RR> f) => EitherCreators.Right(f(Value));
            public Either<L, RR> Bind<L, RR>(Func<R, Either<L, RR>> f) => f(Value);
        }
    }
}
