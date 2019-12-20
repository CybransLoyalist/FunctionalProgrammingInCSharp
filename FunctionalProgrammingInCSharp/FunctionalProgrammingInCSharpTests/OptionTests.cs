using FunctionalProgrammingInCSharp;
using FunctionalProgrammingInCSharp.ExampleClasses;
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

        [Test]
        public void Map_ShouldWorkProperly()
        {
            Option<int> none = None;
            Option<int> some = Some(5);
            Func<int, string> map = (s) => s.ToString();

            Assert.AreEqual(None, none.Map(map));
            Assert.AreEqual(Some("5"), some.Map(map));
        }

        [Test]
        public void MapWithBind_ShouldWorkProperly()
        {
            Option<int> none = None;
            Option<int> some = Some(5);
            Func<int, string> map = (s) => s.ToString();

            Assert.AreEqual(None, none.MapWithBind(map));
            Assert.AreEqual(Some("5"), some.MapWithBind(map));
        }

        [Test]
        public void ForEach_ShallExecuteActionForSome()
        {
            Option<int> some = Some(5);
            var mock = new Mock<Action<int>>();
            some.ForEach(mock.Object);
            mock.Verify(a => a(5), Times.Once);
        }

        [Test]
        public void ForEach_ShallNotActionForNone()
        {
            Option<int> none = None;
            var mock = new Mock<Action<int>>();
            none.ForEach(mock.Object);
            mock.Verify(a => a(It.IsAny<int>()), Times.Never);
        }

        [Test]
        public void Bind_ShouldWorkProperly_ForNone()
        {
            string ageFromKeyboard = "not an int";
            Option<int> none = ageFromKeyboard.ToInt();
            Func<int, Option<Age>> bind = Age.Of;

            Assert.AreEqual(None, none.Bind(bind));
        }

        [Test]
        public void Bind_ShouldWorkProperly_ForSome()
        {
            string ageFromKeyboard = "25";
            Option<int> none = ageFromKeyboard.ToInt();
            Func<int, Option<Age>> bind = Age.Of;

            Assert.AreEqual(Age.Of(25), none.Bind(bind));
        }

        [Test]
        public void WhereShallGiveNone_ForNoneAndIfPredicateNotMatched_AndSomeIfMatched()
        {
            Option<int> none = None;
            Option<int> some = 5;
            Assert.AreEqual(None, none.Where(a => a > 1));
            Assert.AreEqual(None, some.Where(a => a > 10));
            Assert.AreEqual(Some(5), some.Where(a => a > 1));
        }

        [Test]
        public void AsEnumerable_ShallGiveEmptyCollectionIfNone()
        {
            Option<int> none = None;
            CollectionAssert.IsEmpty(none.AsEnumerable());
        }

        [Test]
        public void AsEnumerable_ShallGiveOneElementCollectionIfSome()
        {
            Option<int> some = 5;
            CollectionAssert.AreEqual(new int[] { 5 }, some.AsEnumerable());
        }
    }
}
