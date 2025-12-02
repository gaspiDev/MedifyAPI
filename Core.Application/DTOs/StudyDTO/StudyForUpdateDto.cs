using Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.StudyDTO
{
    public class StudyForUpdateDto
    {
        public string? Notes { get; set; }
        public DateTime? CompletedAt { get; set; }
        public StudyState? State { get; set; }
        public Guid? AppointmentId { get; set; }
    }

}
