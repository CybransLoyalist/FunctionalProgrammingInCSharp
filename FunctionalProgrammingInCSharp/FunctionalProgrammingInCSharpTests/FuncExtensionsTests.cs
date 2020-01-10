using FunctionalProgrammingInCSharp;
using NUnit.Framework;
using System;
using FunctionalProgrammingInCSharp.Excersises6;
using static FunctionalProgrammingInCSharp.OptionCreators;

namespace FunctionalProgrammingInCSharpTests
{
    [TestFixture]
    public class FuncExtensionsTests
    {
        [Test]
        public void TryRun_ShallGetValue_IfNoException()
        {
            Func<int> func = () => 5;
            Exceptional<int> expectedResult = 5;
            Assert.AreEqual(expectedResult, func.TryRun());
        }

        [Test]
        public void TryRun_ShallGetException_IfExceptionThrown()
        {
            var ex = new Exception("error!");
            Func<int> func = () => { throw ex; };
            Exceptional<int> expectedResult = ex;
            Assert.AreEqual(expectedResult, func.TryRun());
        }


        [Test]
        public void RunSafely_ShallGetRight_IfNoException()
        {
            Func<int> func = () => 5;
            Either<string, int> expectedResult = 5;
            Assert.AreEqual(expectedResult, func.RunSafely(ex => ex.Message));
        }

        [Test]
        public void RunSafely_ShallGetLeft_IfException()
        {
            var ex = new Exception("error!");
            Func<int> func = () => { throw ex; };
            Either<string, int> expectedResult = "error!";
            Assert.AreEqual(expectedResult, func.RunSafely(e => e.Message));
        }

        [Test]
        public void ApplyPartialApplication_ShouldWorkFine()
        {
            Func<int, float, string> func = (i, f) => $"int: {i}, float: {f}";
            var applied = func.Apply(5);
            Assert.AreEqual("int: 5, float: 3", applied(3.0f));
        }

        [Test]
        public void Curry_ShouldWorkFine()
        {
            Func<int, float, string> func = (i, f) => $"int: {i}, float: {f}";
            var curried = func.Curry();
            Assert.AreEqual("int: 5, float: 3", curried(5)(3));
        }

        [Test]
        public void ApplyR_ShallApplyToRightMostParameter()
        {
            Func<int, float, string> func = (i, f) => $"int: {i}, float: {f}";
            var applied = func.ApplyR(3.0f);
            Assert.AreEqual("int: 5, float: 3", applied(5));
        }

        [Test]
        public void MultiArgumentApplyTests()
        {
            Func<int, int, int> multiply = (x, y) => x * y;

            var result = Some(5).Map(multiply).Apply(3);
            Assert.AreEqual(Some(15), result);

            Assert.AreEqual(Some(200), Some(multiply).Apply(10).Apply(20));

            Assert.AreEqual(None, Some(multiply).Apply(None).Apply(20));
        }
    }
}
