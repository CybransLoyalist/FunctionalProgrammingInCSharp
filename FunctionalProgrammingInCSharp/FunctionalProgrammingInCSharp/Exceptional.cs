using System;

namespace FunctionalProgrammingInCSharp
{
    public struct Exceptional<T>
    {
        private T _value;
        private bool _isExceptional => _exception != null;
        private readonly Exception _exception;

        public Exceptional(T value)
        {
            _value = value;
            _exception = null;
        }

        private Exceptional(Exception ex)
        {
            _exception = ex;
            _value = default(T);
        }

        public R Match<R>(Func<Exception, R> onException, Func<T, R> onValid)
        {
            return _isExceptional ? onException(_exception) : onValid(_value);
        }

        public static implicit operator Exceptional<T>(T value)
        {
            return new Exceptional<T>(value);
        }

        public static implicit operator Exceptional<T>(Exception ex)
        {
            return new Exceptional<T>(ex);
        }
    }
}
