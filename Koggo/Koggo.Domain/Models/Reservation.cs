
using Koggo.Domain.Interfaces;

namespace Koggo.Domain.Models;

public class Reservation : IBaseModel
{
    public int Id { get; set; }
    public int UserServiceId { get; set; }
    public UserService UserService { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ReservationType ReservationType { get; set; }
    public ApprovalStatus ApprovalStatus { get; set; }
    public int RequesterUserId { get; set; }
    public int HandlerUserId { get; set; }
    public DateTime CreationDate { get; set; }
    public string? Location { get; set; }
    public string? Comment { get; set; }
    public decimal TotalPrice { get; set; }
    public Guid CorrelationId { get; set; }
}

public enum ReservationType
{
    UserReserved,
    Blocked
}

public enum ApprovalStatus
{
    NeedsPayment,
    NeedsProducerApproval,
    NeedsToBeCompleted,
    Completed
}