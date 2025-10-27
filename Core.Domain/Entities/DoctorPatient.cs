using System;
using Core.Domain.Enums;

namespace Core.Domain.Entities;

public class DoctorPatient
{
    public Guid Id { get; set; }
    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
    public required AssociationMethod Method { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime? UnassignedAt { get; set; }

    public Guid DoctorId { get; set; }
    public Doctor Doctor { get; set; } = null!;

    public Guid PatientId { get; set; }
    public Patient Patient { get; set; } = null!;
}
