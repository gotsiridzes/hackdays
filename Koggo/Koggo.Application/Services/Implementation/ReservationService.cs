using System.Runtime.Versioning;
using Koggo.Application.Models.Response;
using Koggo.Domain.Models;
using Koggo.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Koggo.Application.Services.Interface;

public class ReservationService : IReservationService
{
    private ICustomRepository _customRepository; 
    private ILogger<ReservationService> _logger; 
    public ReservationService(ILogger<ReservationService> logger, ICustomRepository customRepository)
    {
        _customRepository = customRepository;
        _logger = logger;
    }

    public async Task<List<ReservationInfo>> GetReservationsAsync(string username, int userId, int userType, CancellationToken cancellationToken)
    {
        var reservationInfos = await _customRepository.GetReservationInfoAsync(userId,cancellationToken);
        if (reservationInfos.IsNullOrEmpty())
        {
            return null;
        }

        foreach (var reservation in reservationInfos)
        {
            reservation.UserName = username;
            reservation.UserType = (UserType)userType;
        }

        return reservationInfos;
    }
}