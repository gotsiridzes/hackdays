using Koggo.Application.Models.Response;
namespace Koggo.Application.Services.Interface;

public interface IReservationService
{
     Task<List<ReservationInfo>> GetReservationsAsync(string username, int userId, int userType);
}
