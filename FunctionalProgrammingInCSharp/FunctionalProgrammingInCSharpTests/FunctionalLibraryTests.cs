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
        public void SortWithCopy_ShallNotModifyExistingCollection()
        {
            var list = new int[] { 1, 5, 2 };
            CollectionAssert.AreEqual(new int[] { 1, 2, 5 }, list.SortWithCopy());
            CollectionAssert.AreEqual(new int[] { 1, 5, 2 }, list);
        }

        [Test]
        public void SortWithCopyWithComparison_ShallNotModifyExistingCollection()
        {
            var list = new int[] { 1, 5, 2, 4 };
            //if we do Comparison of a and b and we get -1, it mean a is smaller, so it should come first in collection
            CollectionAssert.AreEqual(new int[] { 4, 2, 1, 5 }, list.SortWithCopy((a,b) => a % 2 == 0 ? -1 : 1 )); 
            CollectionAssert.AreEqual(new int[] { 1, 5, 2, 4 }, list);
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
    }
}
