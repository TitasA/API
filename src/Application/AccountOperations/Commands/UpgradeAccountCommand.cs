using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.AccountOperations.Commands
{
    public record UpgradeAccountCommand(int AccountId, CustomerType UpgrateToCustomer) : IRequest<Account>;

    public class UpgradeAccountCommanddHandler : IRequestHandler<UpgradeAccountCommand, Account>
    {
        private readonly IAccountOperationDbContext _accountOperationDbContext;

        public UpgradeAccountCommanddHandler(IAccountOperationDbContext accountOperationDbContext)
        {
            _accountOperationDbContext = accountOperationDbContext;
        }

        public async Task<Account> Handle(UpgradeAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountOperationDbContext
                .Accounts
                .FirstOrDefaultAsync(x => x.Id == request.AccountId);

            if (account == null)
            {
                throw new ArgumentNullException($"Account {request.AccountId} not found");
            }

            account.UpgrateToCustomer(request.UpgrateToCustomer);

            await _accountOperationDbContext.SaveChangesAsync(cancellationToken);

            return account;
        }
    }
}
