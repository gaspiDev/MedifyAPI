using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.UserDTO
{
    public class UserForViewDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Auth0Id { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
