using FunctionalProgrammingInCSharp;
using FunctionalProgrammingInCSharp.Excersises4;
using NUnit.Framework;
using System.Collections.Generic;

namespace FunctionalProgrammingInCSharpTests
{
    [TestFixture]
    public class DictionaryExtensionsTests
    {
        [Test]
        public void MapShallWorkProperly()
        {
            var input = new Dictionary<int, string>
            {
                [1] = "a",
                [2] = "b",
                [3] = "c",
            };
            IDictionary<int, string> result = input.Map( v => v.ToUpper());

            CollectionAssert.AreEqual(new Dictionary<int, string>
            {
                [1] = "A",
                [2] = "B",
                [3] = "C",
            }, result);
        }
    }
}
