using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.AssociationDTO
{
    public class AssociationForViewDto
    {
        public Guid Id { get; set; }
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
        public DateTime AssignedAt { get; set; }
        public string Method { get; set; } = string.Empty;
    }

}
