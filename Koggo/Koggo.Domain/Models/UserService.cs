using Koggo.Domain.Records;

namespace Koggo.Domain.Models;

public class UserService
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ServiceId { get; set; }
    public Money Price { get; set; } = null!;
    public TimeOnly AvailableStartHour { get; set; }
    public TimeOnly AvailableEndHour { get; set; }
}