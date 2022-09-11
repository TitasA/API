using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class AccountOperationDbContext : DbContext, IAccountOperationDbContext
    {
        public AccountOperationDbContext(DbContextOptions<AccountOperationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Account> Accounts => Set<Account>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasMany(d => d.Transactions)
                .WithOne(p => p.Account)
                .HasForeignKey(d => d.AccountId)
                .Metadata.DependentToPrincipal
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            modelBuilder.Entity<Account>()
                .HasDiscriminator<Enum>(x => x.CustomerType)
                .HasValue<Account>(CustomerType.Regular)
                .HasValue<AccountVIP>(CustomerType.VIP);

            modelBuilder.Entity<Account>().HasData(
                new List<Account>()
                {
                    new Account(1, Currency.EUR, Status.Open),
                    new Account(2, Currency.USD, Status.Open),
                    new Account(3, Currency.EUR, Status.Closed),
                });

            modelBuilder.Entity<AccountVIP>().HasData(
                new List<Account>()
                {
                    new AccountVIP(4, Currency.EUR, Status.Open, 0.01M),
                    new AccountVIP(5, Currency.USD, Status.Open, 0.05M),
                    new AccountVIP(6, Currency.EUR, Status.Closed, 0.01M),
                });

            modelBuilder.Entity<Transaction>().HasData(
                new List<Transaction>()
                {
                    new Transaction(1, 1, Currency.EUR, new DateTime(2022, 09, 11), OperationType.Deposit, 100),
                    new Transaction(1, 2, Currency.EUR, new DateTime(2022, 09, 11), OperationType.Deposit, 100),
                    new Transaction(2, 3, Currency.USD, new DateTime(2022, 09, 11), OperationType.Deposit, 50),
                    new Transaction(3, 4, Currency.EUR, new DateTime(2022, 09, 11), OperationType.Deposit, 100),
                });
        }
    }
}
