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
    private IRepository<Reservation> _RegistrationRepository; 
    private IRepository<User> _userRepository; 
    private ILogger<ReservationService> _logger; 
    public ReservationService(ILogger<ReservationService> logger, IRepository<Reservation> repository, IRepository<User> userRepository)
    {
        _RegistrationRepository = repository;
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<List<ReservationInfo>> GetReservationsAsync(string username, int userId, int userType)
    {
        var reservations = await _RegistrationRepository.GetWithoutTrackingAsync(x => x.UserService.UserId == userId, CancellationToken.None,
            new string[]{"UserService","Service"});
        if (reservations.IsNullOrEmpty())
        {
            return null;
        }

        var response = new List<ReservationInfo>();
        foreach (var reservation in reservations)
        { 
            response.Add(new ReservationInfo
            {
                Id = reservation.Id,
                StartDate =  reservation.StartDate ,
                EndDate =  reservation.EndDate,
                ReservationType = reservation.ReservationType,
                ApprovalStatus =  reservation.ApprovalStatus,
                UserName = username,
                ServiceName = reservation.UserService.Service.Name,
                CreationDate = reservation.CreationDate,
                Location = reservation.Location,
                Comment = reservation.Comment,
                TotalPrice = reservation.TotalPrice
            });
        }

        return response;
    }
}