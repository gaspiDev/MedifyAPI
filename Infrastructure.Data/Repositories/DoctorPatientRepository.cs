using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class DoctorPatientRepository : BaseRepository<DoctorPatient>, IDoctorPatientRepository
    {
        public DoctorPatientRepository(MedifyDbContext context) : base(context) { }

        public async Task<DoctorPatient?> ReadActiveAssociationAsync()
        {
            return await _context.DoctorPatients
                .Where(dp => dp.IsActive)
                .Include(dp => dp.Doctor)
                .Include(dp => dp.Patient)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<DoctorPatient?> ReadExistingAssociationsAsync(Guid doctorId, Guid patientId)
        {
            return await _context.DoctorPatients
                .Where(dp => dp.DoctorId == doctorId && dp.PatientId == patientId && dp.IsActive)
                .Include(dp => dp.Doctor)
                .Include(dp => dp.Patient)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<DoctorPatient?> ReadInactiveAsync(Guid doctorId, Guid patientId)
        {
            return await _context.DoctorPatients
                .Where(dp => dp.DoctorId == doctorId && dp.PatientId == patientId && dp.IsActive == false)
                .Include(dp => dp.Doctor)
                .Include(dp => dp.Patient)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<DoctorPatient?> ReadAssociationByIdAsync(Guid id)
        {
            return await _context.DoctorPatients
                .Where(dp=> dp.IsActive == true)
                .Include(dp => dp.Doctor)
                .Include(dp => dp.Patient)
                .AsNoTracking()
                .FirstOrDefaultAsync(dp => dp.Id == id);
        }

        public async Task<IEnumerable<DoctorPatient>?> ReadAssociationsByDoctorAsync(Guid doctorId)
        {
            return await _context.DoctorPatients
                .Where(dp => dp.DoctorId == doctorId)
                .Include(dp => dp.Patient)
                .ThenInclude(p => p.User)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<DoctorPatient>?> ReadAssociationsByPatientAsync(Guid patientId)
        {
            return await _context.DoctorPatients
                .Where(dp => dp.PatientId == patientId)
                .Include(dp => dp.Doctor)
                .ThenInclude(d => d.User)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<DoctorPatient>?> ReadAllActiveAssociationsAsync()
        {
            return await _context.DoctorPatients
                .Where(dp => dp.IsActive)
                .Include(dp => dp.Doctor)
                .ThenInclude(d => d.User)
                .Include(dp => dp.Patient)
                .ThenInclude(p => p.User)
                .AsNoTracking()
                .ToListAsync();
        }

        // New: patients not actively associated with given doctor
        public async Task<IEnumerable<Patient>?> ReadPatientsNotAssociatedWithDoctorAsync(Guid doctorId)
        {
            return await _context.Patients
                .Include(p => p.User)
                .Where(p => p.User.IsActive &&
                            !_context.DoctorPatients.Any(dp => dp.DoctorId == doctorId && dp.PatientId == p.Id && dp.IsActive))
                .AsNoTracking()
                .OrderBy(p => p.LastName)
                .ThenBy(p => p.FirstName)
                .ToListAsync();
        }

        public async Task<Guid?> DeleteAssociations(Guid id)
        {
            var AssId = await ReadAssociationByIdAsync(id);
            AssId.IsActive = false;
            _context?.SaveChangesAsync();
            return AssId.Id;
        }



    }
}
