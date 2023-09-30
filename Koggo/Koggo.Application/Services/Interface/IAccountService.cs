using Koggo.Application.Models.Request;
using Koggo.Domain.Models;

namespace Koggo.Application.Services.Interface;

public interface IAccountService
{
    public Task<string> LoginAsync(string username, string password, CancellationToken cancellationToken);
    public Task AddAccountAsync(CreateUser request, CancellationToken cancellationToken);
    public Task<User> AddAccountAndReturnAsync(CreateUser request, CancellationToken cancellationToken);
}