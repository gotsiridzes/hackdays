using Koggo.Domain.Interfaces;

namespace Koggo.Domain.Models;

public class User : IBaseModel
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Salt { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public UserType Type { get; set; }
    public ICollection<UserService> UserServices { get; set; }
}

public enum UserType
{
    Consumer,
    Producer
}