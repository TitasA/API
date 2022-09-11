using Domain.Enums;

namespace Domain.Entities
{
    public class Transaction
    {
        public int Id { get; init; }
        public Currency Currency { get; init; }
        public DateTime Date { get; init; }
        public OperationType OperationType { get; init; }
        public decimal Amount { get; init; }

        public Account Account { get; set; } = null!;
        public int AccountId { get; set; }

        public Transaction(Currency currency, DateTime date, OperationType operationType, decimal amount)
        {
            if (amount == 0)
            {
                throw new ArgumentException($"Amount can not be 0.");
            }

            Currency = currency;
            Date = date;
            OperationType = operationType;
            Amount = amount;
        }

        public Transaction(int accountId, int id, Currency currency, DateTime date, OperationType operationType, decimal amount) : this(currency, date, operationType, amount)
        {
            AccountId = accountId;
            Id = id;
        }
    }
}
