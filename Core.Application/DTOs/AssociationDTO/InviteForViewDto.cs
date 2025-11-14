using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.AssociationDTO
{
    public class InviteForViewDto
    {
        public Guid Id { get; set; }
        public string? InviteCode { get; set; }
        public string? QRToken { get; set; }
        public DateTime ExpiresAt { get; set; }
        public Guid DoctorId { get; set; }
    }

}
