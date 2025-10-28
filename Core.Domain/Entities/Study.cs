using System;
using Core.Domain.Enums;
using Core.Domain.Interfaces;

namespace Core.Domain.Entities;

public class Study : IEntity
{
    public Guid Id { get; set; }
    public required string Type { get; set; }
    public required string StudyUrl { get; set; }
    public string? Notes { get; set; }
    public required DateTime StudyDate { get; set; }
    public DateTime? CompletedAt { get; set; }
    public StudyState State { get; set; } = StudyState.Pending;

    public Guid? AppointmentId { get; set; }
    public Appointment? Appointment { get; set; }
    public required Guid DoctorId { get; set; }
    public Doctor Doctor { get; set; } = null!;
    public required Guid PatientId { get; set; }
    public Patient Patient { get; set; } = null!;

}
