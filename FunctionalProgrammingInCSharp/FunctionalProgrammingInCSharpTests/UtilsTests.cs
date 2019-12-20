using FunctionalProgrammingInCSharp;
using static FunctionalProgrammingInCSharp.OptionCreators;
using NUnit.Framework;
using System;

namespace FunctionalProgrammingInCSharpTests
{

    [TestFixture]
    public class UtilsTests
    {
        [Test]
        public void PrependCounter_ShallWorkCorrectly()
        {
            Assert.AreEqual(
                "1. Humbak 2. Orka 3. Płetwal",
                new[] { "Humbak", "Orka", "Płetwal" }.PrependCounter());
        }

        [TestCaseSource("DayOfWeekParsingTestCases")]
        public void DayOfWeek_ShallBeParsedProperly(string input, Option<DayOfWeek> expectedResult)
        {
            Assert.AreEqual(expectedResult, input.ToEnum<DayOfWeek>());
        }

        static object[] DayOfWeekParsingTestCases = new[]
        {
            new object [] {"Monday", (Option<DayOfWeek>)Some(DayOfWeek.Monday)},
            new object [] {"Friday", (Option<DayOfWeek>)Some(DayOfWeek.Friday)},
            new object [] {"Lama", (Option<DayOfWeek>)None},
        };

        [Test]
        public void HeadShallGetFirstElementIfAnyElementPresent()
        {
            var input = new int[] { 1, 4, 7, 10, 15 };
            Assert.AreEqual(Some(1), input.Head());
        }

        [Test]
        public void HeadShallGetNoneIfEmpty()
        {
            var input = new int[0];
            Assert.AreEqual(None, input.Head());
        }

        [Test]
        public void LookupShallGetSomeIfMatchingElementPresent()
        {
            var input = new int[] { 1, 4, 7, 10, 15 };
            Assert.AreEqual(Some(10), input.Lookup(a => a % 5 == 0));
        }

        [Test]
        public void LookupShallGetNoneIfMatchingElementNotPresent()
        {
            var input = new int[] { 1, 4, 7, 12 };
            Assert.AreEqual(None, input.Lookup(a => a % 5 == 0));
        }
    }
}
