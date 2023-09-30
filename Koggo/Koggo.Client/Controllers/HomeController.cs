using System.Diagnostics;
using Koggo.Application.Models.Request;
using Koggo.Application.Services.Interface;
using Koggo.Client.Extensions;
using Microsoft.AspNetCore.Mvc;
using Koggo.Client.Models;
using Koggo.Client.Models.Home;
using Koggo.Domain.Models;
using Koggo.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Koggo.Client.Controllers;

public class HomeController : ControllerBase
{
    private readonly ILogger<HomeController> _logger;
    private readonly IAccountService _accountService;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IRepository<UserService> _usersServiceRepository;
    
    public HomeController(ILogger<HomeController> logger, IAccountService accountService, IJwtTokenService jwtTokenService, IRepository<UserService> usersServiceRepository) : base(jwtTokenService)
    {
        _logger = logger;
        _accountService = accountService;
        _jwtTokenService = jwtTokenService;
        _usersServiceRepository = usersServiceRepository;
    }

    public IActionResult Index()
    {
        var isValid = ValidateToken();
        return View(new BaseControllerModel() {TokenIsValid = isValid});
    }

    public async Task<IActionResult> Services(int page, CancellationToken cancellationToken)
    {
        var isValid = ValidateToken();
        var model = new ServicesModel
        {
            TokenIsValid = isValid
        };
        
        if (!isValid)
            return View(model);

        model.UserServices = await _usersServiceRepository
            .GetByPagesAsync(x => x.Price > 0, page, 10, cancellationToken);

        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Registration()
    {
        return View(new BaseControllerModel(){TokenIsValid = ValidateToken()});
    }

    [HttpPost("Registration")]
    public async Task<IActionResult> CreateUser(CreateUser model, CancellationToken cancellationToken)
    {
        var user = await _accountService.AddAccountAndReturnAsync(model, cancellationToken);

        var token = _jwtTokenService.GetToken(user);
        Response.AddJwtToken(token);
        return RedirectToAction("Services");
    }
    
    [HttpGet("Login")]
    public IActionResult Login()
    {
        var isValid = ValidateToken();
        return View(new BaseControllerModel() {TokenIsValid = isValid});
    }
    
    [HttpPost("Login")]
    public async Task<IActionResult> LoginAsync(string username, string password, CancellationToken cancellationToken)
    {
        var token = await _accountService.LoginAsync(username, password, cancellationToken);
        Response.AddJwtToken(token);
        return RedirectToAction("Index");
    }

    public IActionResult LogOut()
    {
        Response.RemoveJwtToken();
        return RedirectToAction("Services");
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}