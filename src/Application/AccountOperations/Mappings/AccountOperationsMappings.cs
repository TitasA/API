using Application.AccountOperations.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.AccountOperations.Mappings
{
    public class AccountOperationsMappings : Profile
    {
        public AccountOperationsMappings()
        {
            CreateMap<Account, StatusDto>();

            CreateMap<Account, StatusDto>()
                .ForCtorParam(nameof(StatusDto.AccountId), options => options.MapFrom(account => account.Id))
                .ForCtorParam(nameof(StatusDto.Status), options => options.MapFrom(account => account.Status));
        }
    }
}
