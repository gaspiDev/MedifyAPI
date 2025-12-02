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

        public async Task<Patient?> ReadUserByIdAsync(Guid Id)
        {
            return await _context.Patients
                .Include(p => p.User)
                .Where(u => u.User.IsActive)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == Id);
        }

        public async Task<IEnumerable<Patient>> ReadAllPatientsAsync()
        {
            return await _context.Patients
                .Include(p => p.User)
                .Where(p => p.User.IsActive)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Doctor?>> ReadDoctorsByPatientAsync(Guid patientId)
        {
            return await _context.DoctorPatients
                .Where(dp => dp.PatientId == patientId)
                .Include(dp => dp.Doctor)
                    .ThenInclude(dp => dp.User)
                .Select(dp => dp.Doctor)
                .Where(p => p.User.IsActive)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<int?> DeletePatient(Guid id)
        {
            var patient = await _context.Patients.
                Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (patient != null)
            {
                patient.User.IsActive = false;
            }
            return await _context.SaveChangesAsync();
        }

    }
}
