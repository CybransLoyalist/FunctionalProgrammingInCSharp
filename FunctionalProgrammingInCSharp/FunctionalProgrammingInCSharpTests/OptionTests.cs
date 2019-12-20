using FunctionalProgrammingInCSharp;
using Moq;
using NUnit.Framework;
using System;
using static FunctionalProgrammingInCSharp.OptionCreators;

namespace FunctionalProgrammingInCSharpTests
{
    [TestFixture]
    public class OptionTests
    {
        Mock<Func<string>> onNone;
        Mock<Func<string, string>> onSome;

        [SetUp]
        public void SetUp()
        {
            onNone = new Mock<Func<string>>();
            onSome = new Mock<Func<string, string>>();
        }

        [Test]
        public void Match_ShallCallOnNone_WhenNone()
        {
            Option<string> none = None;

            none.Match(
                onNone.Object,
                onSome.Object);

            onNone.Verify(a => a());
            onSome.Verify(a => a(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void Match_ShallCallOnSome_WhenSome()
        {
            const string word = "hello";
            Option<string> some = word;

            some.Match(
                onNone.Object,
                onSome.Object);

            onNone.Verify(a => a(), Times.Never);
            onSome.Verify(a => a(word));
        }

        [Test]
        public void CreatingOptionFromNull_ShallGiveNone()
        {
            string @null = null;
            Option<string> noneFromNull = @null;
            Option<string> none = None;
            Assert.AreEqual(none, noneFromNull);
        }

        [Test]
        public void NoneOption_ShallBeEqualToNone()
        {
            Option<string> none = None;
            Assert.AreEqual(None, none);
        }

        [Test]
        public void SomeOption_ShallBeEqualToSome()
        {
            Option<string> some = "abc";
            Option<string> some1 = Some("abc");
            Assert.AreEqual(some, some1);
        }
    }
}
