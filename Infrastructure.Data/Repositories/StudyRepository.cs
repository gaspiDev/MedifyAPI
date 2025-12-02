using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class StudyRepository : BaseRepository<Study>, IStudyRepository
    {
        public StudyRepository(MedifyDbContext context) : base(context) { }

        public async Task<IEnumerable<Study>?> ReadStudies()
        {
            return await _context.Studies
                .Include(s => s.Patient)
                    .ThenInclude(p => p.User)
                .Include(s => s.Doctor)
                    .ThenInclude(d => d.User)
                .Include(s => s.Appointment)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task <IEnumerable<Study>?> ReadStudiesByPatientId(Guid patientId)
        {
            return await _context.Studies
                .Include(s => s.Patient)
                    .ThenInclude(p => p.User)
                .Include(s => s.Doctor)
                    .ThenInclude(d => d.User)
                .Include(s => s.Appointment) 
                .AsNoTracking()
                .Where(s => s.PatientId == patientId)
                .ToListAsync();
        }

        public async Task<Study?> ReadStudyById(Guid id)
        {
            var query = _context.Studies
                .Include(s => s.Patient)
                    .ThenInclude(p => p.User)
                .Include(s => s.Doctor)
                    .ThenInclude(d => d.User)
                .Include(s => s.Appointment)
                .AsNoTracking()
                .Where(st => st.Id == id
                             && st.Patient != null
                             && st.Patient.User != null
                             && st.Patient.User.IsActive
                             && st.Doctor != null
                             && st.Doctor.User != null
                             && st.Doctor.User.IsActive);

            return await query.FirstOrDefaultAsync();
        }
    }
}

