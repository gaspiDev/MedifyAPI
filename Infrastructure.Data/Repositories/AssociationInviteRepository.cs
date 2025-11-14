using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class AssociationInviteRepository : BaseRepository<AssociationInvite>, IAssociationInviteRepository
    {
        public AssociationInviteRepository(MedifyDbContext context) : base(context) { }

        public async Task<AssociationInvite?> ReadByCodeAsync(string code)
        {
            return await _context.AssociationInvites
                .FirstOrDefaultAsync(ai => ai.InviteCode == code);
        }

        public async Task<AssociationInvite?> ReadQRAsync(string qrToken)
        {
            return await _context.AssociationInvites
                .FirstOrDefaultAsync(ai => ai.QRToken == qrToken);
        }


    }
}
