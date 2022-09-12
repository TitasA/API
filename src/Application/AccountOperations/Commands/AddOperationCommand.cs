using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.AccountOperations.Commands
{
    public record AddOperationCommand(int AccountId, Currency Currency, decimal Amount, OperationTypeDto operationType, DateTime date) : IRequest<Account>;

    public class AddOperationCommandHandler : IRequestHandler<AddOperationCommand, Account>
    {
        private readonly IAccountOperationDbContext _accountOperationDbContext;

        public AddOperationCommandHandler(IAccountOperationDbContext accountOperationDbContext)
        {
            _accountOperationDbContext = accountOperationDbContext;
        }

        public async Task<Account> Handle(AddOperationCommand request, CancellationToken cancellationToken)
        {
            var account = await GetAccount(request, cancellationToken);

            if (account == null)
            {
                throw new ArgumentNullException($"Account {request.AccountId} not found");
            }

            if (request.operationType == OperationTypeDto.Deposit)
            {
                account.AddDepositTransaction(new Transaction(request.Currency, request.date, OperationType.Deposit, request.Amount));
            }

            if (request.operationType == OperationTypeDto.Payment)
            {
                account.AddPaymentTransaction(new Transaction(request.Currency, request.date, OperationType.Payment, request.Amount));
            }

            await _accountOperationDbContext.SaveChangesAsync(cancellationToken);

            return account;
        }

        //TODO: Idea for refactoring. Move GetAccount to the repository.
        private async Task<Account?> GetAccount(AddOperationCommand request, CancellationToken cancellationToken)
        {
            var customerType = await _accountOperationDbContext
                .Accounts
                .Where(x => x.Id == request.AccountId)
                .Select(x => x.CustomerType)
                .FirstOrDefaultAsync(cancellationToken);

            if (customerType == CustomerType.Regular)
            {
                return await _accountOperationDbContext
                    .Accounts
                    .OfType<Account>()
                    .Include(x => x.Transactions)
                    .Where(x => x.Id == request.AccountId)
                    .FirstOrDefaultAsync(cancellationToken);
            }

            if (customerType == CustomerType.VIP)
            {
                return await _accountOperationDbContext
                    .Accounts
                    .OfType<AccountVIP>()
                    .Include(x => x.Transactions)
                    .Where(x => x.Id == request.AccountId)
                    .FirstOrDefaultAsync(cancellationToken);
            }

            throw new ArgumentException($"Customer type not supported or missing customer {request.AccountId}");
        }
    }
}
