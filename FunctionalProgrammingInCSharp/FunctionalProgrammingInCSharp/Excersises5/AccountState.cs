using System;
using static FunctionalProgrammingInCSharp.OptionCreators;

namespace FunctionalProgrammingInCSharp
{
    public class AccountState : IEquatable<AccountState>
    {
        public decimal Balance { get; }
        public AccountState(decimal balance)
        {
            Balance = balance;
        }

        public override bool Equals(object other)
        {
            return other is AccountState state && Equals(state);
        }

        public bool Equals(AccountState other)
        {
            return Balance == other.Balance;
        }
    }

    public static class Account
    {
        public static Option<AccountState> Debit(this AccountState accountState, decimal amount)
        {
            return accountState.Balance < amount ?
                None : 
                Some(new AccountState(accountState.Balance - amount));
        }
    }
}
