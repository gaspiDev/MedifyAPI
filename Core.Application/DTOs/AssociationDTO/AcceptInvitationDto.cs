using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.AssociationDTO
{
    public class AcceptInvitationDto
    {
        public Guid InviteId { get; set; }
        public Guid PatientId { get; set; }
    }
}
