using FunctionalProgrammingInCSharp;
using NUnit.Framework;
using System;

namespace FunctionalProgrammingInCSharpTests
{
    [TestFixture]
    public class ExceptionalTests
    {
        [Test]
        public void Apply_ShallWorkCorrectly()
        {
            var valid = new Exceptional<Func<int,int>>(x => x * 5);
            Assert.AreEqual(new Exceptional<int>(10), valid.Apply(2));

        }
    }
}
