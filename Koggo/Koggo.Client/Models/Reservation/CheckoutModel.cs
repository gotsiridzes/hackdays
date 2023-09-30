namespace Koggo.Client.Models.Home;

public class CheckoutModel : BaseControllerModel
{
   public List<SimpleServiceInfo> ServiceInfos { get; set; }
   public decimal TotalPrice { get; set; }
}

public class SimpleServiceInfo
{
    public string ServiceName { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string PhoneNumber { get; set; }
}