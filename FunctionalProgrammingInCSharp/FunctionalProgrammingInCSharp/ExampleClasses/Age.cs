using System;
using static FunctionalProgrammingInCSharp.OptionCreators;

namespace FunctionalProgrammingInCSharp.ExampleClasses
{
    public class Age : IEquatable<Age>
    {
        public readonly int NumberOfYears;

        private Age(int numberOfYears)
        {
            if (!IsValid(numberOfYears))
            {
                throw new ArgumentException($"{numberOfYears} is not a valid age");
            }
            NumberOfYears = numberOfYears;
        }

        private static bool IsValid(int numberOfYears)
        {
            return !(numberOfYears < 0 || numberOfYears > 120);
        }

        public static Option<Age> Of(int numberOfYears)
        {
            return IsValid(numberOfYears) ? Some(new Age(numberOfYears)) : None;
        }
        public override bool Equals(object other)
        {
            return Equals(other as Age);
        }

        public bool Equals(Age other)
        {
            return other != null && NumberOfYears == other.NumberOfYears;
        }

        public override int GetHashCode()
        {
            return 1499750128 + NumberOfYears.GetHashCode();
        }
    }
}
