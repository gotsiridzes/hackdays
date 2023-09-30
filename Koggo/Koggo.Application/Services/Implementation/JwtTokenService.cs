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
                new Claim(CustomClaims.UserId,user.Type.ToString())
            }),
            Expires = DateTime.UtcNow.AddMinutes(_options.Value.ExpiryInMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _options.Value.Issuer,
            Audience = _options.Value.Audience
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public void ValidateToken(string jwtToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        tokenHandler.ValidateToken(jwtToken, new TokenValidationParameters(), out _);
    }
}