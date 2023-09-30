using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Koggo.Application.Configuration;
using Koggo.Application.Enums;
using Koggo.Application.Services.Interface;
using Koggo.Domain.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Koggo.Application.Services.Implementation;

public class JwtTokenService : IJwtTokenService
{
    private readonly IOptions<JwtOptions> _options;

    public JwtTokenService(IOptions<JwtOptions> options)
    {
        _options = options;
    }

    public string GetToken(User user)
    {
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_options.Value.Key);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(CustomClaims.UserId,user.Id.ToString()),
                new Claim(CustomClaims.UserType,((int)user.Type).ToString())
            }),
            Expires = DateTime.UtcNow.AddMinutes(_options.Value.ExpiryInMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _options.Value.Issuer,
            Audience = _options.Value.Audience
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public bool ValidateToken(string? jwtToken)
    {
        if (string.IsNullOrWhiteSpace(jwtToken))
        {
            return false;
        }
        
        var key = Encoding.ASCII.GetBytes(_options.Value.Key);

        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidIssuer = _options.Value.Issuer,
            ValidateAudience = true,
            ValidAudience = _options.Value.Audience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromHours(1) // Optional: adjust the allowed clock skew if necessary
        };

        try
        {
            tokenHandler.ValidateToken(jwtToken, validationParameters, out _);
            return true;
        }
        catch
        {
            // Token validation failed
            return false;
        }
    }
}