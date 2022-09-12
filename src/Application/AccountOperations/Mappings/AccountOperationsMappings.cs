using Application.AccountOperations.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.AccountOperations.Mappings
{
    public class AccountOperationsMappings : Profile
    {
        public AccountOperationsMappings()
        {
            CreateMap<Account, StatusDto>()
                .ForCtorParam(nameof(StatusDto.AccountId), options => options.MapFrom(account => account.Id))
                .ForCtorParam(nameof(StatusDto.Status), options => options.MapFrom(account => account.Status));

            CreateMap<Account, BalanceDto>()
                .ForCtorParam(nameof(BalanceDto.AccountId), options => options.MapFrom(account => account.Id))
                .ForCtorParam(nameof(BalanceDto.Currency), options => options.MapFrom(account => account.Currency))
                .ForCtorParam(nameof(BalanceDto.Balance), options => options.MapFrom(account => account.Balance))
                .ForCtorParam(nameof(BalanceDto.Status), options => options.MapFrom(account => account.Status));
        }
    }
}
