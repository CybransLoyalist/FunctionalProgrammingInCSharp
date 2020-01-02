using FunctionalProgrammingInCSharp.Excersises7;
using Moq;
using NUnit.Framework;
using System;

namespace FunctionalProgrammingInCSharpTests
{
    [TestFixture]
    public class LogTests
    {
        [Test]
        public void Logger_ShallLogInfo()
        {
            var loggingMock = new Mock<Action<string>>();
            var logger = LogExtensions.CreateLogger(loggingMock.Object);

            ConsumeLog(logger, "ala ma kota");
            loggingMock.Verify(a => a("message: ala ma kota with severity: Info"));
        }

        void ConsumeLog(Log log, string message)
        {
            log.Info(message);
        }
    }
}
