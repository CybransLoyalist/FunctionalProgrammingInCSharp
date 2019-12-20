using FunctionalProgrammingInCSharp.Option;
using System;

namespace FunctionalProgrammingInCSharp
{
    public struct Option<T> : IEquatable<Option.None>, IEquatable<Some<T>>, IEquatable<Option<T>>
    {
        private T _value;
        private readonly bool _isSome;
        private bool _isNone => !_isSome;

        private Option(T value)
        {
            _value = value;
            _isSome = true;
        }

        public static implicit operator Option<T>(Option.None _)
        {
            return new Option<T>();
        }

        public static implicit operator Option<T>(Option.Some<T> some)
        {
            return new Option<T>(some.Value);
        }

        public static implicit operator Option<T>(T value)
        {
            return value == null ? new Option<T>() : new Option<T>(value);
        }


        public R Match<R>(Func<R> onNone, Func<T, R> onSome)
        {
            return _isSome ? onSome(_value) : onNone();
        }

        public bool Equals(None other)
        {
            return !_isSome;
        }

        public bool Equals(Some<T> other)
        {
            return _isSome && _value.Equals(other.Value);
        }

        public bool Equals(Option<T> other)
        {
            return _isNone && other._isNone ||
                 (_isSome && other._isSome && _value.Equals(other._value));
        }
    }

    public static class OptionCreators
    {
        public static Option.None None => Option.None.Default;
        public static Option<T> Some<T>(T value) => new Option.Some<T>(value);
    }

    namespace Option
    {
        public struct None
        {
            public static readonly None Default = new None();
        }

        public class Some<T>
        {
            public readonly T Value;

            public Some(T value)
            {
                if (value == null)
                {
                    throw new ArgumentException();
                }
                Value = value;
            }
        }
    }
}
