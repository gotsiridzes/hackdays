using Koggo.Domain.Models;

namespace Koggo.Application.Models.Response;

public class ReservationInfo 
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ReservationType ReservationType { get; set; }
    public ApprovalStatus ApprovalStatus { get; set; }
    public string UserName { get; set; }
    public string ServiceName { get; set; }
    public DateTime CreationDate { get; set; }
    public string? Location { get; set; }
    public string? Comment { get; set; }
    public decimal TotalPrice { get; set; }
}