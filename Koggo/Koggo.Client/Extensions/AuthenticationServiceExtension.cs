using System.Text;
using Koggo.Application.Configuration;
using Koggo.Application.Services.Implementation;
using Koggo.Application.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Koggo.Client.Extensions;

public static class AuthenticationServiceExtension
{
    public static IServiceCollection AddJwtAuth(this IServiceCollection services, JwtOptions options)
    {
        var key = Encoding.ASCII.GetBytes(options.Key);
        
        services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = options.Issuer,
                    ValidateAudience = true,
                    ValidAudience = options.Audience,
                    ValidateLifetime = true
                };
            });
        services.AddAuthorization();
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        return services;
    }
}