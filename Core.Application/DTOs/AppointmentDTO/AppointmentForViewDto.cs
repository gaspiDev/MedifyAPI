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
}
