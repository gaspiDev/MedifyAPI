using Core.Application.DTOs.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.DoctorDTO
{
    public class DoctorForViewDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;
        public string LicenseNumber { get; set; } = string.Empty;
        public string Adress { get; set; } = string.Empty;
        public int Dni { get; set; }
        public UserForViewDto User { get; set; } = null!;
    }
}
