using System.Collections;
using System.Linq.Expressions;
using Koggo.Application.Models.Response;
using Koggo.Domain.Interfaces;
using Koggo.Domain.Models;
using Koggo.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Koggo.Infrastructure.Implementation;

public class CustomRepository : ICustomRepository
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly KoggoDbContext _context;
    
    public CustomRepository(KoggoDbContext context)
    {
        _context = context;
        _unitOfWork = context;
    }
    
    public async Task<List<ReservationInfo>> GetReservationInfoAsync(int userId, CancellationToken cancellationToken = default)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();
            var reservationInfo = await _context.Reservations
                .Include(x => x.UserService)
                .ThenInclude(x => x.Service)
                .Where(res => res.RequesterUserId == userId || res.HandlerUserId == userId)
                .Select(info => new ReservationInfo
                    {
                        Id = info.Id,
                        StartDate = info.StartDate,
                        EndDate = info.EndDate,
                        ReservationType = info.ReservationType,
                        ApprovalStatus = info.ApprovalStatus,
                        ServiceName = info.UserService.Service.Name,
                        CreationDate = info.CreationDate,
                        Location = info.Location,
                        Comment = info.Comment,
                        TotalPrice = info.TotalPrice,
                    }
                )
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);


            return reservationInfo;
        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message}");
        }
    }
    

    public void Dispose()
    {
        _unitOfWork.Dispose();
    }

}