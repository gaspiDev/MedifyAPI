using Core.Domain.Interfaces;
using System;

namespace Core.Domain.Entities;

public class AssociationInvite : IEntity
{
    public Guid Id { get; set; }
    public required string InviteCode { get; set; }
    public required string QRToken { get; set; }

    public bool IsAccepted { get; set; } = false;
    public DateTime SentAt { get; set; } = DateTime.UtcNow;
    public DateTime ExpiresAt { get; set; }
    public DateTime? AcceptedAt { get; set; }

    public Guid DoctorId { get; set; }
    public Doctor Doctor { get; set; } = null!;
    public Guid? PatientId { get; set; }
    public Patient? Patient { get; set; }


}
