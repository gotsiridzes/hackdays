using System.Linq.Expressions;
using Koggo.Domain.Ext;
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

    //public async Task<List<UserService>> GetUserServicesByPageAsync(int page, int count)
    //{
    //    var max = await _context.UserServices.CountAsync();
    //    var maxPages = max / count;
    //    var startIndex = page * count > max ? max - (max - (maxPages * count)) : page * count;
    //    var takeCount = (page + 1) * count > max ? max - maxPages * count : count;

    //    return await _context.UserServices
    //        .AsNoTracking()
    //        .Include(x => x.Service)
    //        .OrderBy(x => x.Id)
    //        ;
    //}

    //TODO should implement paging
    public async Task<List<UserService>> GetUserServicesByPageFilteredAsync(int page, int count, decimal? minPrice, decimal? maxPrice, string[]? categories)
    {
	    var max = await _context.UserServices.CountAsync();
	    var maxPages = max / count;
	    var startIndex = page * count > max ? max - (max - (maxPages * count)) : page * count;
	    var takeCount = (page + 1) * count > max ? max - maxPages * count : count;
		IEnumerable<UserService> userServices = _context
		    .UserServices
		    .AsNoTracking()
		    .Include(x => x.Service)
		    .OrderBy(x => x.Id);

	    if (minPrice is not null)
		    userServices = userServices.Where(x => x.Price >= minPrice);

	    if (maxPrice is not null)
		    userServices = userServices.Where(x => x.Price <= maxPrice);

	    if (categories != null && categories.Length != 0)
		    userServices = userServices.Where(x => categories.Contains(x.Service.ServiceType.ToGeo()));

	    return userServices
		    .Skip(startIndex)
		    .Take(takeCount)
		    .ToList();
    }

	public async Task<List<ServiceType>> ListServiceTypesAsync() =>
	    Enum
		    .GetValues(typeof(ServiceType))
		    .Cast<ServiceType>()
		    .ToList();

	public Task<List<UserService>> GetUserServiceByIdsAndIncludes(int[] ids)
	{
		return _context.UserServices
			.Include(x => x.Service)
			.Include(x => x.User)
			.Where(x => ids.Contains(x.Id))
			.ToListAsync();
	}
}