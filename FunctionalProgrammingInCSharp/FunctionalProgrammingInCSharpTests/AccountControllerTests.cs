using FunctionalProgrammingInCSharp;
using Moq;
using NUnit.Framework;
using static FunctionalProgrammingInCSharp.OptionCreators;

namespace FunctionalProgrammingInCSharpTests
{

    [TestFixture]
    public class AccountControllerTests
    {
        private AccountController _cut;
        private Mock<IRepository<AccountState>> _repositoryMock;
        private Mock<IValidator<AccountState>> _validatorMock;
        const int accountId = 5;

        [SetUp]
        public void SetUp()
        {
            _repositoryMock = new Mock<IRepository<AccountState>>();
            _validatorMock = new Mock<IValidator<AccountState>>();
            _cut = new AccountController(
                _repositoryMock.Object, _validatorMock.Object);
        }

        [Test]
        public void MakeTransfer_ShallNotCallValidator_IfNotInTheRepository()
        {
            _repositoryMock.Setup(a => a.Get(accountId)).Returns(None);
            _cut.MakeTransfer((accountId, 100));
            _validatorMock.Verify(a => a.Validate(It.IsAny<AccountState>()), Times.Never);
        }

        [Test]
        public void MakeTransfer_ShallCallValidator_IfPresentInTheRepository()
        {
            var accountState = new AccountState(500);
            _repositoryMock.Setup(a => a.Get(accountId)).Returns(accountState);
            _cut.MakeTransfer((accountId, 100));
            _validatorMock.Verify(a => a.Validate(accountState));
        }

        [Test]
        public void MakeTransfer_ShallNotSaveToRepositoryr_IfNotValid()
        {
            var accountState = new AccountState(500);
            _repositoryMock.Setup(a => a.Get(accountId)).Returns(accountState);
            _validatorMock.Setup(a => a.Validate(accountState)).Returns(None);
            _cut.MakeTransfer((accountId, 100));
            _repositoryMock.Verify(a => a.Save(It.IsAny<AccountState>()), Times.Never);
        }

        [Test]
        public void MakeTransfer_ShallSaveToRepositoryr_IfValid()
        {
            var accountState = new AccountState(500);
            _repositoryMock.Setup(a => a.Get(accountId)).Returns(accountState);
            _validatorMock.Setup(a => a.Validate(accountState)).Returns(accountState);
            _cut.MakeTransfer((accountId, 100));
            _repositoryMock.Verify(a => a.Save(accountState));
        }
    }
}
