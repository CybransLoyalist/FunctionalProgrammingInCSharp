using FunctionalProgrammingInCSharp.Excersises7;
using NUnit.Framework;
using System.Linq;

namespace FunctionalProgrammingInCSharpTests
{
    [TestFixture]
    public class AggregateExcersisesTests
    {
        [Test]
        public void Map_ShallMap()
        {
            var list = Enumerable.Range(1, 3);
            var mapped = list.MapWithAggregate(i => i.ToString());
            CollectionAssert.AreEqual(new string[] { "1", "2", "3" }, mapped );
        }

        [Test]
        public void Where_ShallFilter()
        {
            var list = Enumerable.Range(1, 4);
            var mapped = list.WhereWithAggregate(a => a % 2 == 0);
            CollectionAssert.AreEqual(new [] { 2,4 }, mapped);
        }

        [Test]
        public void Bind_ShallFlatten()
        {
            var list = Enumerable.Range(1, 3);
            var mapped = list.BindWithAggregate(a => Enumerable.Range(10, a));
            CollectionAssert.AreEqual(new[] { 10, 10, 11, 10, 11, 12 }, mapped);
        }
    }
}
