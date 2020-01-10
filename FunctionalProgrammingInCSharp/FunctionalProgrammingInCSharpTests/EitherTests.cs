using FunctionalProgrammingInCSharp;
using Moq;
using NUnit.Framework;
using System;
using static FunctionalProgrammingInCSharp.EitherCreators;
using static FunctionalProgrammingInCSharp.OptionCreators;

namespace FunctionalProgrammingInCSharpTests
{
    [TestFixture]
    public class EitherTests
    {
        [Test]
        public void Right_ShallBeRight()
        {
            Either<string, int> eitherIntOrMessage = 5;
            Assert.IsTrue(eitherIntOrMessage.IsRight);
        }

        [Test]
        public void Right_ShallBeRight_WhenUsingExplicitType()
        {
            Either<int, int> eitherIntOrAlsoInt = Right(5);
            Assert.IsTrue(eitherIntOrAlsoInt.IsRight);
        }

        [Test]
        public void Left_ShallNotBeRight()
        {
            Either<string, int> eitherIntOrMessage = "error";
            Assert.False(eitherIntOrMessage.IsRight);
        }

        [Test]
        public void Left_ShallNotBeRight_WhenUsingExplicitType()
        {
            Either<int, int> eitherIntOrAlsoInt = Left(5);
            Assert.False(eitherIntOrAlsoInt.IsRight);
        }

        [Test]
        public void Match_ShallCallRightMethod_WhenRight()
        {
            Either<string, int> eitherIntOrMessage = 5;

            Assert.AreEqual(25, eitherIntOrMessage.Match(
                s => s.Length,
                a => a * a));
        }

        [Test]
        public void Match_ShallCallLeftMethod_WhenNotRight()
        {
            var errorMessage = "some error here";
            Either<string, int> eitherIntOrMessage = "some error here";

            Assert.AreEqual(errorMessage.Length, eitherIntOrMessage.Match(
                s => s.Length,
                a => a * a));
        }

        [Test]
        public void Map_ShallMapRightValue()
        {
            Either<string, int> eitherIntOrMessage = 5;
            Either<string, int> expectedResult = Right(25);
            Assert.AreEqual(expectedResult, eitherIntOrMessage.Map(a => a * a));
        }

        [Test]
        public void Map_ShallSkip_IfNotRight()
        {
            var calledOnRight = new Mock<Func<int, int>>();
            Either<string, int> eitherIntOrMessage = "error";
            eitherIntOrMessage.Map(calledOnRight.Object);

            calledOnRight.Verify(a => a(It.IsAny<int>()), Times.Never);
        }

        [Test]
        public void Bind_ShallSkip_IfNotRight()
        {
            var calledOnRight = new Mock<Func<int, Either<string, int>>>();
            Either<string, int> eitherIntOrMessage = "error";
            eitherIntOrMessage.Bind(calledOnRight.Object);

            calledOnRight.Verify(a => a(It.IsAny<int>()), Times.Never);
        }

        [Test]
        public void Bind_Example_Right()
        {
            Either<string, int> eitherIntOrMessage = 25;
            Func<int, Either<string, double>> bindFunc = i =>
            i - 2.5 > 0 ? (Either<string, double>)Right(i - 2.5) : Left("negative!");

            var result = eitherIntOrMessage.Bind(bindFunc);

            Assert.AreEqual(22.5, result.Match(s => 0, r => r));
        }

        [Test]
        public void Bind_Example_Left()
        {
            Either<string, int> eitherIntOrMessage = 1;
            Func<int, Either<string, double>> bindFunc = i =>
            i - 2.5 > 0 ? (Either<string, double>)Right(i - 2.5) : Left("negative!");

            var result = eitherIntOrMessage.Bind(bindFunc);

            Assert.AreEqual(0, result.Match(s => 0, r => r));
        }

        [Test]
        public void Foreach_ShallCallAction()
        {
            var calledOnRight = new Mock<Action<int>>();
            Either<string, int> eitherIntOrMessage = 10;
            eitherIntOrMessage.ForEach(calledOnRight.Object);

            calledOnRight.Verify(a => a(10));
        }

        [Test]
        public void EitherBind_RealExample()
        {
            var wakeUpResult = WakeUpEarly(true);
            var groceriesResult = wakeUpResult.Bind(GetGroceries(false));
            var cookingResult = groceriesResult.Bind(Cook(false));

            Assert.AreEqual("failed to get groceries", cookingResult.Match(l => l, r => "success"));

            Either<string, string> WakeUpEarly(bool willSucceed)
            {
                return willSucceed ?
                    (Either<string, string>)Right("success") :
                    (Either<string, string>)Left("overslept");
            }

            Func<string, Either<string, string>> GetGroceries(bool willSucceed)
            {
                if (willSucceed)
                {
                    return s => (Either<string, string>)Right($"Groceries: {s}");
                }
                return l => (Either<string, string>)Left("failed to get groceries");
            }

            Func<string, Either<string, string>> Cook(bool willSucceed)
            {
                if (willSucceed)
                {
                    return s => (Either<string, string>)Right($"Cooking: {s}");
                }
                return l => (Either<string, string>)Left("failed to cook");
            }
        }

        [Test]
        public void ToOption_ShallDiscardLeftValue_IfPresent()
        {
            Either<string, int> eitherIntOrMessage = "error";
            var option = eitherIntOrMessage.ToOption();
            Assert.AreEqual(None, option);
        }

        [Test]
        public void ToOption_ShallUseRightValue_IfRight()
        {
            Either<string, int> eitherIntOrMessage = 1;
            var option = eitherIntOrMessage.ToOption();
            Assert.AreEqual(Some(1), option);
        }

        [Test]
        public void ShallBeAbleToBind_OptionReturningMethod_WithEitherReturning()
        {
            Func<int, Either<string, int>> func1 = i => Right(i);
            Func<int, Either<string, int>> func2 = i => Right(i * 2);

            var bound = Right(2).Bind(func1).Bind(func2);

            Assert.AreEqual((Either<string, int>)Right(4), bound);

            Func<int, Option<int>> func3OptionReturning = i => Some(i * 3);
            var bound2 = Right(2).Bind(func1).Bind(func3OptionReturning);
            Assert.AreEqual((Either<string, int>)Right(6), bound2);

            var bound3 = ((Either<string, int>)Right(2)).Bind(func3OptionReturning).Bind(func1);
            Assert.AreEqual((Either<string, int>)Right(6), bound3);
        }
    }
}
