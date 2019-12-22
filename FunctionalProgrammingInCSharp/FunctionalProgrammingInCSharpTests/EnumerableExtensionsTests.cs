using FunctionalProgrammingInCSharp;
using FunctionalProgrammingInCSharp.ExampleClasses;
using static FunctionalProgrammingInCSharp.OptionCreators;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctionalProgrammingInCSharpTests
{

    [TestFixture]
    public class EnumerableExtensionsTests
    {
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
            CollectionAssert.AreEqual(new int[] { 4, 2, 1, 5 }, list.SortWithCopy((a, b) => a % 2 == 0 ? -1 : 1));
            CollectionAssert.AreEqual(new int[] { 1, 5, 2, 4 }, list);
        }

        [Test]
        public void BindShallFlattenNestedListsResult()
        {
            Func<int, IEnumerable<int>> func = (x) => Enumerable.Range(1, x);
            var arr = new int[] { 1, 3, 5 };
            var result = arr.Bind(func);
            CollectionAssert.AreEqual(
                new int[] { 1, 1, 2, 3, 1, 2, 3, 4, 5 }, result);
        }

        [Test]
        public void MapShallWorkProperly()
        {
            var input = new int[] { 1, 2, 4 };
            CollectionAssert.AreEqual(new string[] { "1", "2", "4" }, input.Map(a => a.ToString()));
        }

        [Test]
        public void MapWithBindShallWorkProperly()
        {
            var input = new int[] { 1, 2, 4 };
            CollectionAssert.AreEqual(new string[] { "1", "2", "4" }, input.MapWithBind(a => a.ToString()));
        }

        [Test]
        public void BindShallFlattenOptionsAsEnumerables_ToListContainingOnlySomes()
        {
            IEnumerable<Option<Age>> subjects = new[]
            {
                Age.Of(20),
                None,
                Age.Of(120)
            };
            var withoutNones = subjects.Bind(a => a.AsEnumerable());
            var averageAge = withoutNones.Select(a => a.NumberOfYears).Average();
            Assert.AreEqual(70, averageAge);
        }

        [Test]
        public void MyTake_ShallWorkProperly()
        {
            var input = Enumerable.Range(1, 10);
            CollectionAssert.AreEqual(Enumerable.Range(1, 5), input.MyTake(5));
        }

        [Test]
        public void MyOrderBy_ShallWorkProperly()
        {
            var input = Enumerable.Range(1, 10);
            CollectionAssert.AreEqual(Enumerable.Range(1, 10).Reverse(), input.MyOrderBy((a, b) => a > b ? -1 : 1));
        }

        [Test]
        public void MyAverage_ShallWorkProperly()
        {
            var input = Enumerable.Range(1, 10);
            Assert.AreEqual(5, input.MyAverage());
        }
    }
}
