using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.AssociationDTO
{
    public class AcceptByCodeDto
    {
        public string InviteCode { get; set; } = string.Empty;
    }
    public class AcceptByQrDto
    {
        public string QrToken { get; set; } = string.Empty;
    }
}
