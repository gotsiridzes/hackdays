
using Koggo.Domain.Interfaces;

namespace Koggo.Domain.Models;

public class UserService : IBaseModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ServiceId { get; set; }
    public decimal Price { get; set; }
    public DateTime AvailableStartHour { get; set; }
    public DateTime AvailableEndHour { get; set; }
}