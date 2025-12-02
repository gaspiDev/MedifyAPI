using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.DoctorDTO
{
    public class DoctorForUpdateDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Specialty { get; set; }
        public string? LicenseNumber { get; set; }
        public string? Adress { get; set; }
        public int? Dni { get; set; }
    }
}
