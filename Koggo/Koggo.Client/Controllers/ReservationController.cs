using System.Diagnostics;
using Koggo.Application.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Koggo.Client.Models;
using Microsoft.AspNetCore.Authorization;

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

    public IActionResult Index()
    {
        
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}

