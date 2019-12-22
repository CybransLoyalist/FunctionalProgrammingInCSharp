using FunctionalProgrammingInCSharp.ExampleClasses;
using Moq;
using NUnit.Framework;
using System;
using static FunctionalProgrammingInCSharp.FunctionalLibrary;
using Unit = System.ValueTuple;

namespace FunctionalProgrammingInCSharpTests
{

    [TestFixture]
    public class FunctionalLibraryTests
    {
        [Test]
        public void Negate_ShallNegateThePredicate()
        {
            Func<int, bool> predicate = x => x % 2 == 0;
            Assert.True(predicate(4));
            Assert.False(predicate.Negate()(4));
        }

        [Test]
        public void UsingShallDoWhatExpectedWithDisposable_AndThenGetDisposed()
        {
            var loggerMock = new Mock<ILogger>();

            Assert.AreEqual(
                "I'm doing something useful.",
                Using(() => new Disposable(loggerMock.Object), d => d.DoBeforeDisposing()));

            loggerMock.Verify(a => a.Log("I'm disposing myself. Good bye."));
        }

        [Test]
        public void ToFunc_ShallCreateFuncFromAction()
        {
            var actionMock = new Mock<Action>();
            Assert.AreEqual(new Unit(), actionMock.Object.ToFunc()());
            actionMock.Verify(a => a());
        }

        [Test]
        public void ToFunc_ShallCreateFuncFromActionWithOneParameter()
        {
            var actionMock = new Mock<Action<int>>();
            int param = 5;
            Assert.AreEqual(new Unit(), actionMock.Object.ToFunc()(param));
            actionMock.Verify(a => a(param));
        }

        [Test]
        public void Compose_ShallWorkProperly()
        {
            Func<int, int> power = x => x * x;
            Func<int, string> toStringAndAppendAbc = x => x.ToString() + "abc";
            Func<string, string> toUpper = x => x.ToUpper();

            var result = power.Compose(toStringAndAppendAbc).Compose(toUpper)(5);
            Assert.AreEqual("25ABC", result);
        }
    }
}
