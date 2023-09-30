using System.Linq.Expressions;
using Koggo.Application.Models.Response;
using Koggo.Domain.Interfaces;
using Koggo.Domain.Models;

namespace Koggo.Infrastructure.Interfaces;

public interface ICustomRepository
{
    Task<List<ReservationInfo>> GetReservationInfoAsync(int userId, CancellationToken cancellationToken = default);
}