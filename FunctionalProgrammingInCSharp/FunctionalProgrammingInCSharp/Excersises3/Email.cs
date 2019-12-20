using System;
using System.Text.RegularExpressions;
using static FunctionalProgrammingInCSharp.OptionCreators;

namespace FunctionalProgrammingInCSharp
{
    public class Email : IEquatable<Email>
    {
        public readonly string Address;
        private static Regex emailValidationRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

        private Email(string address)
        {
            Address = address;
        }

        public static Option<Email> Create(string address)
        {
            Match match = emailValidationRegex.Match(address);
            if (!match.Success)
            {
                return None;
            }

            return new Email(address);
        }

        public bool Equals(Email other)
        {
            return Address == other.Address;
        }

        public static implicit operator string(Email email)
        {
            return email.Address;
        }
    }
}
