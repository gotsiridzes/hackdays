using System.Linq.Expressions;
using Koggo.Domain.Models;

namespace Koggo.Infrastructure.Interfaces;

public interface IUserServiceRepository
{
    //public Task<List<UserService>> GetUserServicesByPageAsync(int page, int count);
    public Task<List<UserService>> GetUserServicesByPageFilteredAsync(int page, int count, decimal? minPrice, decimal? maxPrice, string[]? categories);
    public Task<List<ServiceType>> ListServiceTypesAsync();
    public Task<List<UserService>> GetUserServiceByIdsAndIncludes(int[] ids);
}