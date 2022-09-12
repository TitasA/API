using Application.AccountOperations.Dtos;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.AccountOperations.Queries
{
    public record GetBalanceQuery(int AccountId) : IRequest<BalanceDto?>;

    public class GetBalanceQueryHandler : IRequestHandler<GetBalanceQuery, BalanceDto?>
    {
        private readonly IAccountOperationDbContext _accountOperationDbContext;
        private readonly IMapper _mapper;

        public GetBalanceQueryHandler(IAccountOperationDbContext accountOperationDbContext, IMapper mapper)
        {
            _accountOperationDbContext = accountOperationDbContext;
            _mapper = mapper;
        }

        public async Task<BalanceDto?> Handle(GetBalanceQuery request, CancellationToken cancellationToken)
        {
            var accountDto = await _accountOperationDbContext
                .Accounts
                .Include(x => x.Transactions)
                .AsNoTracking()
                .Where(x => x.Id == request.AccountId)
                .ProjectTo<BalanceDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);

            return accountDto;
        }
    }
}
