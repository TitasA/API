using Domain.Entities;
using Domain.Enums;

namespace Tests
{
    public class AccountUnitTests
    {
        private readonly Transaction transation;

        public AccountUnitTests()
        {
            transation = new Transaction(Currency.EUR, DateTime.Now, OperationType.Deposit, 10);
        }

        [Fact]
        public void ShouldThrow_WhenAccountIsClosed()
        {
            var sut = new Account(Currency.EUR, Status.Closed);

            Assert.Throws<InvalidOperationException>(() => sut.UpgrateToCustomer(CustomerType.VIP));
            Assert.Throws<InvalidOperationException>(() => sut.AddPaymentTransaction(transation));
            Assert.Throws<InvalidOperationException>(() => sut.AddDepositTransaction(transation));
        }

        [Fact]
        public void ShouldThrow_WhenBalanceBecomesNegative()
        {
            var sut = new Account(Currency.EUR, Status.Open);

            sut.AddDepositTransaction(transation);

            Assert.Throws<InvalidOperationException>(() => sut.AddPaymentTransaction(new Transaction(Currency.EUR, DateTime.Now, OperationType.Deposit, -11)));
        }

        [Fact]
        public void ShouldAllowBalanceToBecome0()
        {
            var sut = new Account(Currency.EUR, Status.Open);

            sut.AddDepositTransaction(transation);
            sut.AddPaymentTransaction(new Transaction(Currency.EUR, DateTime.Now, OperationType.Deposit, -10));

            Assert.Equal(sut.Balance, decimal.Zero);
        }

        [Fact]
        public void ShouldAcceptPaymentAndDeposit()
        {
            var sut = new Account(Currency.EUR, Status.Open);

            sut.AddDepositTransaction(transation);
            sut.AddPaymentTransaction(new Transaction(Currency.EUR, DateTime.Now, OperationType.Deposit, -10));

            Assert.Equal(sut.Balance, decimal.Zero);
        }

        [Fact]
        public void ShouldCorrectlyCalculateCashBack()
        {
            const int payment = -10;
            const decimal cashBack = 0.1M;
            var sut = new AccountVIP(Currency.EUR, Status.Open, cashBack);

            sut.AddDepositTransaction(transation);
            sut.AddPaymentTransaction(new Transaction(Currency.EUR, DateTime.Now, OperationType.Deposit, payment));

            Assert.Equal(sut.Balance, Math.Abs(payment * cashBack));
        }
    }
}