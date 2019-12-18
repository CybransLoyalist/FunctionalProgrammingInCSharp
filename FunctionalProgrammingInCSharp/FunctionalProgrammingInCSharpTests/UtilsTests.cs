using FunctionalProgrammingInCSharp;
using NUnit.Framework;

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
    }
}
