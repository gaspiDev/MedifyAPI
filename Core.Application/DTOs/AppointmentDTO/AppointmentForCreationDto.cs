using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.AppointmentDTO
{
    internal class AppointmentForCreationDto
    {
        public DateTime AppointmentDate { get; set; }
        public string? Reason { get; set; }
        public string? Diagnosis { get; set; }

        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
    }
}
