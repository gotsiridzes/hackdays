using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Koggo.Infrastructure.Interfaces;

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken);
    DatabaseFacade Database { get; }
}