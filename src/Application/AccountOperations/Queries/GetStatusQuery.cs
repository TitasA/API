using Application.AccountOperations.Dtos;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.AccountOperations.Queries
{
    public record GetStatusQuery(int AccountId) : IRequest<StatusDto>;

    public class GetStatusQueryHandler : IRequestHandler<GetStatusQuery, StatusDto?>
    {
        private readonly IAccountOperationDbContext _accountOperationDbContext;
        private readonly IMapper _mapper;

        public GetStatusQueryHandler(IAccountOperationDbContext accountOperationDbContext, IMapper mapper)
        {
            _accountOperationDbContext = accountOperationDbContext;
            _mapper = mapper;
        }

        public async Task<StatusDto?> Handle(GetStatusQuery request, CancellationToken cancellationToken)
        {
            var statusDto = await _accountOperationDbContext
                .Accounts
                .AsNoTracking()
                .Where(x => x.Id == request.AccountId)
                .ProjectTo<StatusDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);

            return statusDto;
        }
    }
}
