using Koggo.Domain.Models;

namespace Koggo.Application.Services.Interface;

public interface IJwtTokenService
{
    public string GetToken(User user);
}