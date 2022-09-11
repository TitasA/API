using Domain.Enums;

namespace Domain.Entities
{
    public class AccountVIP : Account
    {
        public decimal CashBack { get; private set; }

        public AccountVIP(Currency currency, Status status, decimal cashBack) : base(currency, status)
        {
            CashBack = cashBack;
            CustomerType = CustomerType.VIP;
        }

        public AccountVIP(int id, Currency currency, Status status, decimal cashBack) : base(id, currency, status)
        {
            CashBack = cashBack;
            CustomerType = CustomerType.VIP;
        }

        public override void AddPaymentTransaction(Transaction transaction)
        {
            base.AddPaymentTransaction(transaction);

            _transactions.Add(new Transaction(transaction.Currency, DateTime.Now, OperationType.CashBack, Math.Abs(transaction.Amount) * CashBack));
        }

        public void UpdateCashBack(decimal cashBack)
        {
            if (cashBack <= 0)
            {
                throw new ArgumentException("cashBack can not be 0 or negative");
            }

            CashBack = cashBack;
        }
    }
}
