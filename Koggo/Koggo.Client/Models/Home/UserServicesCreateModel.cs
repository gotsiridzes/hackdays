namespace Koggo.Client.Models.Home
{
    public class UserServicesCreateModel : BaseControllerModel
    {
        public List<UserServicesCreateItem> UserServices { get; set; }
    }

    public class UserServicesCreateItem
    {
        public int UserId { get; set; }
        public int ServiceId { get; set; }
        public decimal Price { get; set; }
        public string? ThumbNailPhoto { get; set; }
        public DateTime AvailableStartHour { get; set; }
        public DateTime AvailableEndHour { get; set; }
    }
}
