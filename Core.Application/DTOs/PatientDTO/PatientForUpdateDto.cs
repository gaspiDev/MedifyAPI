using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.PatientDTO
{
    public class PatientForUpdateDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Dni { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
