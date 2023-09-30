using Koggo.Application.Helpers;
using Koggo.Application.Models.Request;
using Koggo.Application.Services.Interface;
using Koggo.Domain.Models;
using Koggo.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Koggo.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _service;

    public AccountController(IAccountService service)
    {
        _service = service;
    }

    [HttpPost("Login")]
    public Task<string> LoginAsync([FromQuery] string username, [FromQuery] string pass, CancellationToken cancellationToken)
        => _service.LoginAsync(username, pass, cancellationToken);

    [Authorize]
    [HttpPost("AddUser")]
    public Task AddAccountAsync(CreateUser request, CancellationToken cancellationToken)
        => _service.AddAccountAsync(request, cancellationToken);
}