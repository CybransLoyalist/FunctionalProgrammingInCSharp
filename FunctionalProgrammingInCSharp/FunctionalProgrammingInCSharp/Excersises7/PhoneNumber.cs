using FunctionalProgrammingInCSharp.Excersises6;
using System;

namespace FunctionalProgrammingInCSharp.Excersises7
{
    public class CountryCode
    {
        public string Value { get; }
        private CountryCode(string v)
        {
            Value = v;
        }

        public static implicit operator CountryCode(string v) => new CountryCode(v);
        public static implicit operator string(CountryCode c) => c.Value;
    }
    public class PhoneNumber
    {
        public string Number { get; }
        public CountryCode CountryCode { get; }
        public string NumberType { get; }

        private PhoneNumber(string number, string countryCode, string numberType)
        {
            Number = number;
            CountryCode = countryCode;
            NumberType = numberType;
        }

        public Func<string, string, string, PhoneNumber> Create() =>
            (countryCode, numberType, number) => new PhoneNumber(number, countryCode, numberType);

        public Func<string, string, PhoneNumber> CreateUK(string number, string numberType) =>
          Create().Apply("uk");

        public Func<string, PhoneNumber> CreateUKMobile(string number) =>
          Create().Apply("uk").Apply("mobile");
    }
}
