using FunctionalProgrammingInCSharp;
using NUnit.Framework;
using System;
using System.Text;
using static FunctionalProgrammingInCSharp.OptionCreators;

namespace FunctionalProgrammingInCSharpTests
{

    [TestFixture]
    public class EmailTests
    {
        const string address = "abc@gmail.com";

        [Test]
        public void EmailShouldBeConvertibleToString()
        {
            Assert.DoesNotThrow(() => Email.Create(address).Match(
                () => throw new Exception(),
                (address) => new StringBuilder(address)));
        }

        [TestCase("lama")]
        [TestCase("lama.com")]
        [TestCase("lama@com")]
        public void InvalidEmailConstruction_ShallGetNone(string invalidAddress)
        {
            Assert.AreEqual(None, Email.Create(invalidAddress));
        }

        [TestCase("lama.is.a.nice.lama@lama.pl")]
        public void ValidEmailConstruction_ShallWork(string validAddress)
        {
            Assert.AreEqual(validAddress, Email.Create(validAddress).Match(
               () => throw new Exception(),
               (a) => a).Address);
        }
    }
}
