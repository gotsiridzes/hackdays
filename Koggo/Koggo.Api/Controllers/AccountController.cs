using Koggo.Application.Helpers;
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
    private readonly KoggoDbContext _context;
    private readonly IJwtTokenService _jwtTokenService;
    
    public AccountController(KoggoDbContext context, IJwtTokenService jwtTokenService)
    {
        _context = context;
        _jwtTokenService = jwtTokenService;
    }

    [HttpPost("Login")]
    public async Task<string> LoginAsync([FromQuery]string username, [FromQuery] string pass)
    {
        var user = await _context.Users.FirstAsync(x => x.Username == username).ConfigureAwait(false);

        var hash = HashHelper.HashPassword(pass, user.Salt);

        if (hash != user.Password)
            throw new Exception();

        return _jwtTokenService.GetToken(user);
    }
    
    [Authorize]
    [HttpPost("AddUser")]
    public async Task AddAccountAsync(string username, string password)
    {
        var salt = HashHelper.GetSalt();
        var passwordHash = HashHelper.HashPassword(password, salt);

        var user = new User
        {
            Username = username,
            Password = passwordHash,
            Salt = salt,
            FirstName = "test",
            LastName = "test",
            Email = "test",
            Phone = "555",
            Type = UserType.Consumer
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync().ConfigureAwait(false);
    }
}