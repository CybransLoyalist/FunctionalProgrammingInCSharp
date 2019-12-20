using FunctionalProgrammingInCSharp;
using NUnit.Framework;
using System;
using static FunctionalProgrammingInCSharp.OptionCreators;

namespace FunctionalProgrammingInCSharpTests
{
    [TestFixture]
    public class AppConfigTests
    {
        private AppConfig config = new AppConfig(new System.Collections.Specialized.NameValueCollection()
        {
            ["number"] = "5"
        });
        
        [Test]
        public void IfPresentAndParsable_ResultShallBeSome()
        {
            Assert.AreEqual(Some(5), config.Get<int>("number"));
        }

        [Test]
        public void IfPresentBotNotParsable_ResultShallBeNone()
        {
            Assert.AreEqual(None, config.Get<DateTime>("number"));
        }

        [Test]
        public void IfNotPresent_ResultShallBeNone()
        {
            Assert.AreEqual(None, config.Get<DateTime>("datetime"));
        }

        [Test]
        public void WithDefaultValue_IfPresentAndParsable_ResultShallBeSome()
        {
            Assert.AreEqual(5, config.Get("number", 10));
        }

        [Test]
        public void WithDefaultValue_IfPresentBotNotParsable_ResultShallBeDefault()
        {
            DateTime defaultValue = new DateTime(2019, 12, 20);
            Assert.AreEqual(defaultValue, config.Get<DateTime>("number", defaultValue));
        }

        [Test]
        public void WithDefaultValue_IfNotPresent_ResultShallBeNone()
        {
            DateTime defaultValue = new DateTime(2019, 12, 20);
            Assert.AreEqual(defaultValue, config.Get("datetime", defaultValue));
        }
    }
}
