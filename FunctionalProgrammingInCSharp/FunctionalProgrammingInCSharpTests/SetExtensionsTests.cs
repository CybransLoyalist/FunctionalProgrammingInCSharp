using FunctionalProgrammingInCSharp;
using FunctionalProgrammingInCSharp.Excersises4;
using NUnit.Framework;
using System.Collections.Generic;

namespace FunctionalProgrammingInCSharpTests
{
    [TestFixture]
    public class SetExtensionsTests
    {
        [Test]
        public void MapShallWorkProperly()
        {
            ISet<int> input = new HashSet<int> { 1, 2, 4 };
            CollectionAssert.AreEqual(new HashSet<string> { "1", "2", "4" }, input.Map(a => a.ToString()));
        }
    }
}
