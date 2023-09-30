
using Koggo.Domain.Interfaces;

namespace Koggo.Domain.Models;

public class UserService : IBaseModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public int ServiceId { get; set; }
    public Service Service { get; set; } = null!;
    public decimal Price { get; set; }
    public string? ThumbNailPhoto { get; set; }
    public DateTime AvailableStartHour { get; set; }
    public DateTime AvailableEndHour { get; set; }
    public ICollection<Reservation> Reservations { get; set; } = null!;
}