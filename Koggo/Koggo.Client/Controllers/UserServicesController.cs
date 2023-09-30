using Koggo.Application.Services.Interface;
using Koggo.Client.Models.Home;
using Koggo.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Koggo.Client.Controllers;

public class UserServicesController : ControllerBase
{
    private readonly IUserServiceRepository _userServiceRepository;
    public UserServicesController(IJwtTokenService jwtTokenService, IUserServiceRepository userServiceRepository) : base(jwtTokenService)
    {
        _userServiceRepository = userServiceRepository;
    }
    
    public async Task<IActionResult> Index(int page, CancellationToken cancellationToken)
    {
        var isValid = ValidateToken();
        var model = new UserServicesModel
        {
            TokenIsValid = isValid
        };
        
        if (!isValid)
            return View(model);

        model.UserServices = await _userServiceRepository
            .GetUserServicesByPageAsync(page, 10);

        return View(model);
    }

}