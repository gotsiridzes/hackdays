namespace Koggo.Domain.Records;

public record Money()
{
    public string Currency { get; set; } = null!;
    public decimal Amount { get; set; }
}