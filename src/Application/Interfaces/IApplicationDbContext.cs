using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IAccountOperationDbContext
    {
        DbSet<Account> Accounts { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
