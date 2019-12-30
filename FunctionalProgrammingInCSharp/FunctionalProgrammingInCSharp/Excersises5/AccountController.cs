namespace FunctionalProgrammingInCSharp
{
    public class AccountController
    {
        public AccountController(
            IRepository<AccountState> repository, IValidator<AccountState> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        private IRepository<AccountState> _repository;
        private IValidator<AccountState> _validator;
        public void MakeTransfer((int accountId, decimal amount) transferData)
        {
            _repository
                .Get(transferData.accountId)
                .Bind(Validate)
                .ForEach(WireFunds);
        }

        private void WireFunds(AccountState accountstate)
        {
            _repository.Save(accountstate);
        }

        private Option<AccountState> Validate(AccountState accountstate)
        {
            return _validator.Validate(accountstate);
        }
    }
}
