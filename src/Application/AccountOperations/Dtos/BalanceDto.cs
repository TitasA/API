using Domain.Enums;

namespace Application.AccountOperations.Dtos
{
    public record BalanceDto(int AccountId, Currency Currency, decimal Balance, Status Status);
}
