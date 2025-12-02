using Core.Application.DTOs.DoctorDTO;
using Core.Application.DTOs.PatientDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.AppointmentDTO
{
    public class AppointmentForViewDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string? Reason { get; set; }
        public string? Diagnosis { get; set; }

        public DoctorForViewDto Doctor { get; set; } = null!;
        public PatientForViewDto Patient { get; set; } = null!;
    }
    public class AppointmentForViewAsDoctorDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string? Reason { get; set; }
        public string? Diagnosis { get; set; }

        public string DoctorName { get; set; }
        public Guid DoctorId { get; set; }
        public PatientForViewDto Patient { get; set; } = null!;
    }
    public class AppointmentForViewAsPatientDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string? Reason { get; set; }
        public string? Diagnosis { get; set; }

        public DoctorForViewDto Doctor { get; set; } = null!;
        public string PatientName { get; set; } = null!;
        public Guid PatientId { get; set; }
    }
}
