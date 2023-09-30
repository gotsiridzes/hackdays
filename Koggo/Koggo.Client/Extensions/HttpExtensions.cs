using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Koggo.Application.Enums;
using Microsoft.AspNetCore.Identity;

namespace Koggo.Client.Extensions;

public static class HttpExtensions
{
    public static void AddJwtToken(this HttpResponse response, string token)
    {
        response.Cookies.Append("JwtToken", token);
    }

    public static void RemoveJwtToken(this HttpResponse response)
    {
        response.Cookies.Delete("JwtToken");
    }

    public static string? GetJwtToken(this HttpRequest request)
    {
        return request.Cookies["JwtToken"];
    }

    public static (string Username, string userId, string UserType) ReadJwtTokenInfo(this HttpRequest request)
    {
        var token = request.Cookies["JwtToken"];
        var securityToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

        var userName = securityToken.Claims.FirstOrDefault(x => x.Type == CustomClaims.UserName)?.Value;
        var userId = securityToken.Claims.FirstOrDefault(x => x.Type == CustomClaims.UserId)?.Value;
        var userType = securityToken.Claims.FirstOrDefault(x => x.Type == CustomClaims.UserType)?.Value;

        return (userName, userId, userType);
    }
}