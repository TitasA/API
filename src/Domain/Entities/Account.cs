using Domain.Enums;

namespace Domain.Entities
{
    public class Account
    {
        public int Id { get; init; }
        public Currency Currency { get; init; }
        public Status Status { get; init; }
        public CustomerType CustomerType { get; protected set; }

        protected readonly List<Transaction> _transactions = new List<Transaction>();
        public IReadOnlyCollection<Transaction> Transactions => _transactions;
        public decimal Balance => _transactions.Sum(item => item.Amount);

        public Account(Currency currency, Status status)
        {
            Currency = currency;
            Status = status;
            CustomerType = CustomerType.Regular;
        }

        public Account(int id, Currency currency, Status status) : this(currency, status)
        {
            Id = id;
        }

        public void AddDepositTransaction(Transaction transaction)
        {
            ThrowIfClosedAccount();
            ThrowIfCurrencyDoesNotMatch(transaction);

            if (transaction.Amount <= 0)
            {
                throw new InvalidOperationException("Amount can't be 0 or negative");
            }

            _transactions.Add(transaction);
        }

        public virtual void AddPaymentTransaction(Transaction transaction)
        {
            ThrowIfClosedAccount();
            ThrowIfCurrencyDoesNotMatch(transaction);

            if (transaction.Amount >= 0)
            {
                throw new InvalidOperationException("Amount can't be 0 or positive");
            }

            var difference = Balance - Math.Abs(transaction.Amount);
            if (decimal.Compare(difference, 0) == -1)
            {
                throw new InvalidOperationException($"Can't add payment Transaction. Balance is negative. For account {Id}");
            }

            _transactions.Add(transaction);
        }

        public void UpgrateToCustomer(CustomerType customerType)
        {
            ThrowIfClosedAccount();

            if (CustomerType == customerType)
            {
                throw new InvalidOperationException($"Customer type is already {CustomerType}");
            }

            if ((int)CustomerType > (int)customerType)
            {
                throw new InvalidOperationException($"Can not downgrade customer type to {customerType} from {CustomerType}");
            }

            CustomerType = customerType;
        }

        private void ThrowIfClosedAccount()
        {
            if (Status == Status.Closed)
            {
                throw new InvalidOperationException($"Account {Id} is closed");
            }
        }

        private void ThrowIfCurrencyDoesNotMatch(Transaction transaction)
        {
            if (transaction.Currency != Currency)
            {
                throw new InvalidOperationException($"Only accepting {Currency} currency");
            }
        }
    }
}