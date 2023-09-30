using Koggo.Domain.Models;

namespace Koggo.Client.Models.Home;

public class ServicesModel : BaseControllerModel
{
    public List<UserService> UserServices { get; set; } = null!;
}