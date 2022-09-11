using Application.AccountOperations.Dtos;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.AccountOperations.Queries
{
    public record GetBalanceQuery(int AccountId) : IRequest<BalanceDto?>;

    public class GetBalanceQueryHandler : IRequestHandler<GetBalanceQuery, BalanceDto?>
    {
        private readonly IAccountOperationDbContext _accountOperationDbContext;

        public GetBalanceQueryHandler(IAccountOperationDbContext accountOperationDbContext)
        {
            _accountOperationDbContext = accountOperationDbContext;
        }

        public async Task<BalanceDto?> Handle(GetBalanceQuery request, CancellationToken cancellationToken)
        {
            var account = await _accountOperationDbContext
                .Accounts
                .Include(x => x.Transactions)
                .AsNoTracking()
                .Where(x => x.Id == request.AccountId)
                .SingleOrDefaultAsync(cancellationToken);

            if (account == null)
            {
                return null;
            }

            return new BalanceDto(account.Id, account.Currency, account.Balance, account.Status);
        }
    }
}
