using Koggo.Application.Models.Response;
using Koggo.Domain.Models;

namespace Koggo.Client.Models.Home
{
    public class ReservationModel : BaseControllerModel
    {
        public List<ReservationInfo>? ReservationInfos { get; set; } = null!;
        public UserType UserType { get; set; } 

    }
}
