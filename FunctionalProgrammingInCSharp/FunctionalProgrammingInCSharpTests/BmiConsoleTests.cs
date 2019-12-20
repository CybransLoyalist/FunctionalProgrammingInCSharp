using FunctionalProgrammingInCSharp.Excersises2;
using NUnit.Framework;
using System;
using Moq;

namespace FunctionalProgrammingInCSharpTests
{

    [TestFixture]
    public class BmiConsoleTests
    {
        private readonly BmiConsole _cut = new BmiConsole();
        private Mock<Func<string>> readMock;
        private Mock<Action<string>> writeMock;

        [SetUp]
        public void SetUp()
        {
            readMock = new Mock<Func<string>>();
            writeMock = new Mock<Action<string>>();
        }

        [Test]
        public void ShallKeepAskingAboutHeight_UntilValidDoubleIsPassed()
        {
            readMock.SetupSequence(a => a()).Returns("invalid double").Returns("1,6").Returns("51");

            _cut.Run(readMock.Object, writeMock.Object);

            writeMock.Verify(a => a("What is your height in meters?"), Times.Exactly(2));
        }


        [Test]
        public void ShallKeepAskingAboutWeight_UntilValidDoubleIsPassed()
        {
            readMock.SetupSequence(a => a()).Returns("1,6").Returns("invalid double").Returns("51");

            _cut.Run(readMock.Object, writeMock.Object);

            writeMock.Verify(a => a("What is your weight in kilograms?"), Times.Exactly(2));
        }

        [Test]
        public void ShallWriteProperBmi()
        {
            readMock.SetupSequence(a => a()).Returns("1,6").Returns("51");
            _cut.Run(readMock.Object, writeMock.Object);
            writeMock.Verify(a => a("Your BMI is 19,921875"));
        }


        [Test]
        public void ShallWriteProperBmiResult()
        {
            readMock.SetupSequence(a => a()).Returns("1,6").Returns("51");
            _cut.Run(readMock.Object, writeMock.Object);
            writeMock.Verify(a => a("Your are at healthy weight"));
        }
    }
}
