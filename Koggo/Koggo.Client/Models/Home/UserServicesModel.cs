using Koggo.Domain.Models;

namespace Koggo.Client.Models.Home;

public class UserServicesModel : BaseControllerModel
{
    public List<UserService> UserServices { get; set; } = null!;
    public List<ServiceType> ServiceTypes { get; set; } = null!;
}