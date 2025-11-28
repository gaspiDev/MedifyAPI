using Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.StudyDTO
{
  public class StudyForViewDto
{
    public Guid Id { get; set; }
    public string Type { get; set; } = null!;
    public string StudyUrl { get; set; } = null!;
    public string? FileName { get; set; }
    public string? Notes { get; set; }
    public DateTime StudyDate { get; set; }
    public DateTime? CompletedAt { get; set; }
    public StudyState State { get; set; }

    public Guid DoctorId { get; set; }
    public string DoctorName { get; set; } = null!;

    public Guid PatientId { get; set; }
    public string PatientName { get; set; } = null!;

    public Guid? AppointmentId { get; set; }
}

}
