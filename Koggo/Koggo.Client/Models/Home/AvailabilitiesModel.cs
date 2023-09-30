namespace Koggo.Client.Models.Home
{
    public class AvailabilitiesModel : BaseControllerModel
    {
        public List<SimpleServiceInfo> SimpleServiceInfos { get; set; }
        public List<DateTime> Availabilities { get; set; }
        public decimal TotalPrice { get; set; }
        public string SelectedUserServiceIds { get; set; }
    }
}
