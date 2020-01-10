using FunctionalProgrammingInCSharp;
using FunctionalProgrammingInCSharp.ExampleClasses;
using Moq;
using NUnit.Framework;
using System;
using static FunctionalProgrammingInCSharp.OptionCreators;
using static FunctionalProgrammingInCSharp.EitherCreators;

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
        public void MultiArgumentMap_ShouldWorkProperly()
        {
            Option<int> none = None;
            Option<int> some = Some(5);
            Func<int, int, int> mapMultiply = (a, b) => a * b;
 
            Assert.AreEqual(None, none.Map(mapMultiply));
            Assert.AreEqual(15, some.Map(mapMultiply).Match(() => 0, x => x(3)));
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

        [Test]
        public void ToEither_ShallSetLeftValue_IfNone()
        {
            Option<int> option = None;
            var either = option.ToEither("lacking value");
            Assert.False(either.IsRight);
            Either<string, int> expectedResult = Left("lacking value");
            Assert.AreEqual(expectedResult, either);
        }

        [Test]
        public void ToEither_ShallSetRightValue_IfSome()
        {
            Option<int> option = Some(5);
            var either = option.ToEither("lacking value");
            Assert.True(either.IsRight);
            Either<string, int> expectedResult = Right(5);
            Assert.AreEqual(expectedResult, either);
        }

        [Test]
        public void ShallBeAbleToBind_OptionReturningMethod_WithEitherReturning()
        {
            Func<int, Option<int>> func1 = i => Some(i);
            Func<int, Option<int>> func2 = i => Some(i * 2);

            var bound = Some(2).Bind(func1).Bind(func2);

            Assert.AreEqual(Some(4), bound);

            Func<int, Either<string, int>> func3EitherReturning = i => Right(i * 3);
            var bound2 = Some(2).Bind(func1).Bind(func3EitherReturning);
            Assert.AreEqual(Some(6), bound2);

            var bound3 = Some(2).Bind(func3EitherReturning).Bind(func1);
            Assert.AreEqual(Some(6), bound3);
        }

        [Test]
        public void RightIdentityLaw()
        {
            var some = Some(5);
            Assert.AreEqual(some, some.Bind(Some));
        }

        [Test]
        public void LeftIdentityLaw()
        {
            Func<int, Option<int>> bindFunc = i => Some(i * 2);
            Assert.AreEqual(bindFunc(5), Some(5).Bind(bindFunc));
        }
        
        [Test]
        public void AssociativityLaw()
        {
            Func<int, Option<int>> bindFunc1 = i => Some(i * 2);
            Func<int, Option<int>> bindFunc2 = i => Some(i + 2);
            Assert.AreEqual(
                Some(5).Bind(x => bindFunc1(x).Bind(bindFunc2)), 
                Some(5).Bind(bindFunc1).Bind(bindFunc2));
        }
    }
}
