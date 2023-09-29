namespace Koggo.Application.Configuration;

public class JwtOptions
{
    public const string OptionName = "JwtSettings"; 
    public string Key { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public int ExpiryInMinutes { get; set; }
}