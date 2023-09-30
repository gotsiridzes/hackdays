using System.Linq.Expressions;
using Koggo.Domain.Models;
using Koggo.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Koggo.Infrastructure.Implementation;

public class UserServiceRepository : IUserServiceRepository
{
    private readonly KoggoDbContext _context;

    public UserServiceRepository(KoggoDbContext context)
    {
        _context = context;
    }

    public async Task<List<UserService>> GetUserServicesByPageAsync(int page, int count)
    {
        var max = await _context.UserServices.CountAsync();
        var maxPages = max / count;
        var startIndex = page * count > max ? max - (max - (maxPages * count)) : page * count;
        var takeCount = (page + 1) * count > max ? max - maxPages * count : count;

        return await _context.UserServices
            .AsNoTracking()
            .Include(x => x.Service)
            .OrderBy(x => x.Id)
            .Skip(startIndex)
            .Take(takeCount)
            .ToListAsync();
    }

    public List<UserService> GetUserServicesByPageFiltered(Expression<Func<UserService, bool>> filter)
    {
        throw new NotImplementedException();
    }
}