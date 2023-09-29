using System.Security.Authentication;
using Koggo.Application.Helpers;
using Koggo.Application.Models.Request;
using Koggo.Application.Services.Interface;
using Koggo.Domain.Models;
using Koggo.Infrastructure.Interfaces;

namespace Koggo.Application.Services.Implementation;

public class AccountService : IAccountService
{
    private readonly IRepository<User> _usersRepository;
    private readonly IJwtTokenService _jwtTokenService;
    
    public AccountService(IRepository<User> usersRepository, IJwtTokenService jwtTokenService)
    {
        _usersRepository = usersRepository;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<string> LoginAsync(string username, string password, CancellationToken cancellationToken)
    {
        var user = await _usersRepository
            .GetFirstOrDefaultNoTrackingAsync(x => x.Username == username, cancellationToken)
            .ConfigureAwait(false);

        var hash = HashHelper.HashPassword(password, user.Salt);

        if (hash != user.Password)
            throw new AuthenticationException(); // TODO normal error handling later

        return _jwtTokenService.GetToken(user);
    }

    public async Task AddAccountAsync(CreateUser request, CancellationToken cancellationToken)
    {
        var salt = HashHelper.GetSalt();
        var passwordHash = HashHelper.HashPassword(request.Password, salt);

        var user = new User
        {
            Username = request.Username,
            Password = passwordHash,
            Salt = salt,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Phone = request.Phone,
            Type = request.Type
        };

        await _usersRepository.Add(user, cancellationToken).ConfigureAwait(false);
    }
}