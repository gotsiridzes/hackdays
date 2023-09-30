using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Azure.Core.Pipeline;
using Koggo.Application.Enums;
using Koggo.Application.Models.Response;
using Koggo.Application.Services.Interface;
using Koggo.Client.Extensions;
using Microsoft.AspNetCore.Mvc;
using Koggo.Client.Models;
using Koggo.Client.Models.Home;
using Koggo.Domain.Models;
using Koggo.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace Koggo.Client.Controllers;

public class ReservationController : Controller
{
    private readonly ILogger<ReservationController> _logger;
    private readonly IReservationService _reservationService;
    private readonly IUserServiceRepository _userServiceRepository;
    
    public ReservationController(ILogger<ReservationController> logger, IReservationService reservationService, IUserServiceRepository userServiceRepository)
    {
        _logger = logger;
        _reservationService = reservationService;
        _userServiceRepository = userServiceRepository;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var tokenInfo = HttpExtensions.ReadJwtTokenInfo(HttpContext.Request);
        if (!int.TryParse(tokenInfo.userId, out int userId) || 
            !int.TryParse(tokenInfo.UserType, out int userType) || 
            tokenInfo.Username.IsNullOrEmpty())
        {
            return View(new ReservationModel());
        }
       
        var reservations = await _reservationService
            .GetReservationsAsync(tokenInfo.Username, userId, userType, cancellationToken);
        
        return View(new ReservationModel() {ReservationInfos = reservations ,UserType = UserType.Producer});
    }

    [HttpGet("Reservation/Checkout")]
    public async Task<IActionResult> Checkout(string[] cartItemsRaw)
    {
        var cartItems = cartItemsRaw[0].Split(',');
        var idsCovnerted = new int[cartItems.Length];
        for (var i = 0; i < cartItems.Length; i++)
        {
            var converted = int.Parse(cartItems[i]);
            idsCovnerted[i] = converted;
        }

        var data = await _userServiceRepository.GetUserServiceByIdsAndIncludes(idsCovnerted);

        var viewData = new CheckoutModel() {ServiceInfos = new List<SimpleServiceInfo>()};
        
        foreach (var item in data)
        {
            viewData.ServiceInfos.Add(new SimpleServiceInfo
            {
                ServiceName = item.Service.Name,
                Description = item.Service.Description,
                Price = item.Price,
                PhoneNumber = item.User.Phone
            });
        }

        viewData.TotalPrice = viewData.ServiceInfos.Sum(x => x.Price);
        
        return View(viewData);
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}

