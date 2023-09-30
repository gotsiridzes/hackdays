using System.Linq.Expressions;
using Koggo.Domain.Models;

namespace Koggo.Infrastructure.Interfaces;

public interface IUserServiceRepository
{
    public Task<List<UserService>> GetUserServicesByPageAsync(int page, int count);
    public List<UserService> GetUserServicesByPageFiltered(Expression<Func<UserService, bool>> filter);
}