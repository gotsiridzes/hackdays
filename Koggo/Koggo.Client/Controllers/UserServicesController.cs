using Koggo.Application.Models;
using Koggo.Application.Services.Interface;
using Koggo.Client.Extensions;
using Koggo.Client.Models.Home;
using Koggo.Infrastructure;
using Koggo.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Koggo.Client.Controllers;

public class UserServicesController : ControllerBase
{
    private readonly IUserServiceRepository _userServiceRepository;
    private readonly KoggoDbContext context;

    public UserServicesController(IJwtTokenService jwtTokenService, IUserServiceRepository userServiceRepository, KoggoDbContext context) : base(jwtTokenService)
    {
        _userServiceRepository = userServiceRepository;
        this.context = context;
    }

    public async Task<IActionResult> Index(int page, SearchUserServices req, CancellationToken cancellationToken)
    {
        var isValid = ValidateToken();
        var model = new UserServicesModel
        {
            TokenIsValid = isValid
        };

        if (!isValid)
            return View(model);

        model.UserServices = await _userServiceRepository
            .GetUserServicesByPageFilteredAsync(
                page,
                10,
                req.MinPrice,
                req.MaxPrice,
                req.Categories);

        model.ServiceTypes = await _userServiceRepository
            .ListServiceTypesAsync();

        return View(model);
    }

    public async Task<IActionResult> Create()
    {
        var data = context.Services.ToList();
        var model = new ServicesModel()
        {
            TokenIsValid = base.ValidateToken(),
            Services = data
        };
        return View("CreateUserService", model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(UserServicesCreateItem userService)
    {
        var userInfo = HttpContext.Request.ReadJwtTokenInfo();
        context.UserServices.Add(new Domain.Models.UserService()
        {
            UserId = int.Parse(userInfo.userId),
            ServiceId = userService.ServiceId,
            Price = userService.Price,
            ThumbNailPhoto = userService.ThumbNailPhotoBase64,
            AvailableStartHour = userService.AvailableStartHour,
            AvailableEndHour = userService.AvailableEndHour,
        });
        await context.SaveChangesAsync();
        return RedirectToAction("Index", "Services");
    }
}