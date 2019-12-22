using FunctionalProgrammingInCSharp;
using NUnit.Framework;
using static FunctionalProgrammingInCSharp.OptionCreators;

namespace FunctionalProgrammingInCSharpTests
{
    [TestFixture]
    public class BankAccountTests
    {
        [Test]
        public void DebitingWhenFundsAvailable_ShallGetTheNewAccountState_SmallerByAmount()
        {
            Assert.AreEqual(
                Some(new AccountState(900)),
                new AccountState(1000).Debit(100));
        }

        [Test]
        public void DebitingWhenNotEnoughtFundsAvailable_ShallGetNone()
        {
            Assert.AreEqual(
                None,
                new AccountState(50).Debit(100));
        }
    }
}
