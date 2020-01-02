using FunctionalProgrammingInCSharp;
using NUnit.Framework;
using System;
using FunctionalProgrammingInCSharp.Excersises6;

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
        public void RunSafely_ShallGetLeftt_IfException()
        {
            var ex = new Exception("error!");
            Func<int> func = () => { throw ex; };
            Either<string, int> expectedResult = "error!";
            Assert.AreEqual(expectedResult, func.RunSafely(e => e.Message));
        }
    }
}
