using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class DoctorRepository : BaseRepository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(MedifyDbContext context) : base(context) { }

        public async Task<Doctor?> ReadByLicenseAsync(string licenseNumber)
        {
            return await _context.Doctors
                .Include(d => d.User)
                .Where(d => d.User.IsActive)
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.LicenseNumber == licenseNumber);
        }
        public new async Task<Doctor?> ReadById(Guid id)
        {
            return await _context.Doctors
                .Include(u => u.User)
                .Where(u => u.User.IsActive)
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<Patient>?> ReadPatientsByDoctorAsync(Guid doctorId)
        {
            return await _context.DoctorPatients
                .Where(dp => dp.DoctorId == doctorId)
                .Include(dp => dp.Patient)
                .ThenInclude(dp => dp.User)
                .Select(dp => dp.Patient)
                .Where(p => p.User.IsActive)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Doctor>> ReadAllDoctorsAsync()
        {
            return await _context.Doctors
                .Include(d => d.User)
                .Where(d => d.User.IsActive)
                .AsNoTracking()
                .ToListAsync();
        }


        public async Task<int?> DeleteDoctor(Guid id)
        {
            var doctor = await _context.Doctors
                .Include(d => d.User)
                .FirstOrDefaultAsync(d => d.Id == id);
             doctor.User.IsActive = false;
            return await _context.SaveChangesAsync();
            
        }
    }

        

}
