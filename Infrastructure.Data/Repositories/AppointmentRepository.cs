using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class AppointmentRepository : BaseRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(MedifyDbContext context) : base(context) { }

        public new async Task<IEnumerable<Appointment>> ReadAsync()
        {
            return await _context.Appointments
                .Include(a => a.Doctor).ThenInclude(d => d.User)
                .Include(a => a.Patient).ThenInclude(p => p.User)
                .AsNoTracking()
                .ToListAsync();
        }

        public new async Task<Appointment?> ReadByIdAsync(Guid id)
        {
            return await _context.Appointments
                .Include(a => a.Doctor).ThenInclude(d => d.User)
                .Include(a => a.Patient).ThenInclude(p => p.User)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Appointment?>> ReadAppointmentsByDoctorAsync(Guid doctorId)
        {
            return await _context.Appointments
                .Where(a => a.DoctorId == doctorId)
                .Include(a => a.Patient).ThenInclude(p => p.User)
                .OrderByDescending(a => a.AppointmentDate)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment?>> ReadByPatientIdAsync(Guid patientId)
        {
            return await _context.Appointments
                .Where(a => a.PatientId == patientId)
                .Include(a => a.Doctor).ThenInclude(d => d.User)
                .OrderByDescending(a => a.AppointmentDate)
                .AsNoTracking()
                .ToListAsync();
        }

    }
}
