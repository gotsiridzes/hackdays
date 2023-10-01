using Koggo.Application.Services.Interface;
using Koggo.Client.Extensions;
using Koggo.Client.Models;
using Koggo.Client.Models.Home;
using Koggo.Domain.Models;
using Koggo.Infrastructure;
using Koggo.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace Koggo.Client.Controllers;

public class ReservationController : ControllerBase
{
    private readonly ILogger<ReservationController> _logger;
    private readonly IReservationService _reservationService;
    private readonly IUserServiceRepository _userServiceRepository;
    private readonly KoggoDbContext context;

    public ReservationController(IJwtTokenService jwtTokenService, ILogger<ReservationController> logger,
        IReservationService reservationService, IUserServiceRepository userServiceRepository, KoggoDbContext context)
        : base(jwtTokenService)
    {
        _logger = logger;
        _reservationService = reservationService;
        _userServiceRepository = userServiceRepository;
        this.context = context;
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

        return View(new ReservationModel()
        {
            TokenIsValid = ValidateToken(),
            ReservationInfos = reservations, 
            UserType = (UserType) userType
        });
    }

    [HttpGet("Reservation/Checkout")]
    public async Task<IActionResult> Checkout(string userServiceIds)
    {
        var userServiceIdsSplitted = userServiceIds?.Split(',') ?? new string[0];
        var userServices = context.UserServices
            .Include(x => x.Service)
            .Include(x => x.User)
            .Where(x => userServiceIdsSplitted.Contains(x.ServiceId.ToString())).ToList();
        var viewModel = new AvailabilitiesModel()
        {
            SelectedUserServiceIds = userServiceIds,
            SimpleServiceInfos = new List<SimpleServiceInfo>()
        };

        foreach (var item in userServices)
        {
            viewModel.SimpleServiceInfos.Add(new SimpleServiceInfo
            {
                ServiceName = item.Service.Name,
                Description = item.Service.Description,
                Price = item.Price,
                PhoneNumber = item.User.Phone
            });
        }

        viewModel.TotalPrice = userServices.Sum(x => x.Price);

        var earliestAvailabilities = new List<DateTime>();
        int count = 0;
        var startTime = DateTime.Now;
        while (count < 5 && startTime < DateTime.Now.AddDays(7))
        {
            var hour = startTime.Hour;
            var isOutsideBusinessHours = false;
            foreach (var item in userServices)
            {
                if (!(item.AvailableStartHour.Hour < hour && item.AvailableEndHour.Hour > hour))
                {
                    isOutsideBusinessHours = true;
                    break;
                }

                var reservedTimes = context.Reservations.Where(a => a.HandlerUserId == item.User.Id).ToList();
                foreach (var time in reservedTimes)
                {
                    if ((startTime >= time.StartDate && startTime <= time.EndDate) ||
                        (startTime.AddHours(2) >= time.StartDate && startTime.AddHours(2) <= time.EndDate))
                    {
                        isOutsideBusinessHours = true;
                        break;
                    }
                }
            }

            if (isOutsideBusinessHours)
            {
                startTime = startTime.AddHours(2);
                continue;
            }

            earliestAvailabilities.Add(startTime);
            count++;
            startTime = startTime.AddHours(2);
        }

        earliestAvailabilities.Add(DateTime.Now);
        viewModel.Availabilities = earliestAvailabilities;
        viewModel.TokenIsValid = ValidateToken();
        return View("Checkout", viewModel);
    }

    public async Task<IActionResult> Create(CreateReservationModel request)
    {
        var reservations = new List<Reservation>();
        var userServiceIdsSplitted = request.ServiceIds?.Split(',') ?? new string[0];
        var userServices = context.UserServices
            .Include(x => x.Service)
            .Include(x => x.User)
            .Where(x => userServiceIdsSplitted.Contains(x.ServiceId.ToString())).ToList();
        var guid = Guid.NewGuid();
        var data = Request.ReadJwtTokenInfo();

        foreach (var item in userServices)
        {
            reservations.Add(new Reservation
            {
                UserServiceId = item.Id,
                StartDate = request.DateSelected,
                EndDate = request.DateSelected.AddHours(2),
                ReservationType = ReservationType.UserReserved,
                ApprovalStatus = ApprovalStatus.NeedsPayment,
                RequesterUserId = int.Parse(data.userId),
                HandlerUserId = item.UserId,
                CreationDate = DateTime.Now,
                Location = null,
                Comment = null,
                TotalPrice = item.Price * 1.2M, // TODO required counting logic
                CorrelationId = guid
            });
        }

        context.Reservations.AddRange(reservations);
        await context.SaveChangesAsync();
        return RedirectToAction("Index", "Reservation");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}