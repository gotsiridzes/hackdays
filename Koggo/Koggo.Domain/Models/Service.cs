namespace Koggo.Domain.Models;

public class Service
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public ServiceType ServiceType { get; set; }
}

public enum ServiceType
{
    Tracktor,
    Amwekrani,
    ragaca
}