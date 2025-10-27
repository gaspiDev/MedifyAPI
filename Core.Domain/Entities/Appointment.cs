using System;

namespace Core.Domain.Entities;

public class Appointment
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime AppointmentDate { get; set; }
    public string? Reason { get; set; }
    public string? Diagnosis { get; set; }
    // prescription image

    public required Guid DoctorId { get; set; }
    public Doctor Doctor { get; set; } = null!;

    public required Guid PatientId { get; set; }
    public Patient Patient { get; set; } = null!;
}
