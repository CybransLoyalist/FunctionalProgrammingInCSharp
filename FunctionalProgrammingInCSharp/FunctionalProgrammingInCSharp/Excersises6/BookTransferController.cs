using System;
using static FunctionalProgrammingInCSharp.EitherCreators;
using Unit = System.ValueTuple;

namespace FunctionalProgrammingInCSharp.Excersises6
{
    static class Errors
    {
        public static Error InvalidAccount => new Error("Invalid account");
        public static Error NegativeOrZeroAmount => new Error("Negative or zero amount");
    }
    class Error
    {
        string Message { get; }
        public Error(string message)
        {
            Message = message;
        }
    }

    struct TransferData
    {
        public string AccountNumber;
        public decimal Amount;
    }

    class BookTransferController
    {
        public Either<Error, Unit> BookTransfer(TransferData transferData)
        {
            return Right(transferData)
                .Bind(ValidateAccountNumber)
                .Bind(ValidateAmount)
                .Bind(SaveToRepository);
        }

        private Either<Error, TransferData> ValidateAccountNumber(TransferData transferData)
        {
            if (transferData.AccountNumber.Length < 10)
                return Errors.InvalidAccount;
            return transferData;
        }

        private Either<Error, TransferData> ValidateAmount(TransferData transferData)
        {
            if (transferData.Amount <= 0)
                return Errors.NegativeOrZeroAmount;
            return transferData;
        }

        private Either<Error, Unit> SaveToRepository(TransferData transferData)
        {
            throw new NotImplementedException();
        }
    }
}
