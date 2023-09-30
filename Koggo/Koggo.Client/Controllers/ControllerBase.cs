using Koggo.Application.Services.Interface;
using Koggo.Client.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Koggo.Client.Controllers;

public class ControllerBase : Controller
{
    private readonly IJwtTokenService _jwtTokenService;

    public ControllerBase(IJwtTokenService jwtTokenService)
    {
        _jwtTokenService = jwtTokenService;
    }

    public bool ValidateToken()
    {
        var token = Request.GetJwtToken();
        return _jwtTokenService.ValidateToken(token);
    }
}