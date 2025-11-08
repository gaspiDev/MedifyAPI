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


        public async Task<Patient?> ReadByDniAsync(int dni)
        {
            return await _context.Patients
                .Include(p => p.User)
                .Where(u => u.User.IsActive)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Dni == dni);
        }

        public async Task<Patient?> ReadByUserIdAsync(Guid userId)
        {
            return await _context.Patients
                .Include(p => p.User)
                .Where(u => u.User.IsActive)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task<IEnumerable<Patient>> ReadAllWithUsersAsync()
        {
            return await _context.Patients
                .Include(p => p.User)
                .Where(p => p.User.IsActive)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<int?> DeletePatient(Guid id)
        {
            var patient = await _context.Patients.
                Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == id);
            patient.User.IsActive = false;
            return await _context.SaveChangesAsync();
        }

    }
}
