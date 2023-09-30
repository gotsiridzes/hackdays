using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Azure.Core.Pipeline;
using Koggo.Application.Enums;
using Koggo.Application.Services.Interface;
using Koggo.Client.Extensions;
using Microsoft.AspNetCore.Mvc;
using Koggo.Client.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace Koggo.Client.Controllers;

public class ReservationController : Controller
{
    private readonly ILogger<ReservationController> _logger;
    private readonly IReservationService _reservationService;

    public ReservationController(ILogger<ReservationController> logger, IReservationService reservationService)
    {
        _logger = logger;
        _reservationService = reservationService;
    }

    public async Task<IActionResult> Index()
    {
        var tokenInfo = HttpExtensions.ReadJwtTokenInfo(HttpContext.Request);
        if (!int.TryParse(tokenInfo.userId, out int userId) || 
            !int.TryParse(tokenInfo.UserType, out int userType) || 
            tokenInfo.Username.IsNullOrEmpty())
        {
            throw new Exception("invalid data");
        }
       
        var reservations = await _reservationService
            .GetReservationsAsync(tokenInfo.Username, userId, userType);
        
        return View(reservations);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}

