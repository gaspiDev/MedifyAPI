using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.AssociationDTO
{
    public class AssociationAsSysAdminDto
    {
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
        public string Method { get; set; } = "Manual";
    }
}
