using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class PatientRepository : BaseRepository<Patient>, IPatientRepository
    {
        public PatientRepository(MedifyDbContext context) : base(context) { }


        public async Task<Patient?> GetByDniAsync(int dni)
        {
            return await _context.Patients
                .Include(p => p.User)
                .Where(u => u.User.IsActive)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Dni == dni);
        }

        public async Task<Patient?> GetByUserIdAsync(Guid userId)
        {
            return await _context.Patients
                .Include(p => p.User)
                .Where(u => u.User.IsActive)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task<IEnumerable<Patient>> GetAllWithUsersAsync()
        {
            return await _context.Patients
                .Include(p => p.User)
                .Where(p => p.User.IsActive)
                .AsNoTracking()
                .ToListAsync();
        }

    }
}
