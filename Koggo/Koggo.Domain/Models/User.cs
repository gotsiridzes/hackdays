namespace Koggo.Domain.Models;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public UserType Type { get; set; }
}

public enum UserType
{
    Consumer,
    Producer
}