using Koggo.Domain.Models;

namespace Koggo.Client.Models.Home
{
    public class ServicesModel : BaseControllerModel
    {
        public List<Service> Services { get; set; } = null!;
        public bool CanCreate { get; set; }
    }
}
