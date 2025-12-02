using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.StudyDTO
{
    public class StudyUploadRequest
    {
        public IFormFile? File { get; set; }
        public string Type { get; set; } = null!;
        public string? Notes { get; set; }
        public DateTime StudyDate { get; set; }
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
        public Guid? AppointmentId { get; set; }
    }
}
