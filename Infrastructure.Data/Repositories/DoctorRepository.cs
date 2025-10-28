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

        public async Task<Doctor?> GetByLicenseAsync(string licenseNumber)
        {
            return await _context.Doctors
                .Include(d => d.User)
                .Where(d => d.User.IsActive)
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.LicenseNumber == licenseNumber);
        }

        public async Task<IEnumerable<Doctor>> GetAllWithUsersAsync()
        {
            return await _context.Doctors
                .Include(d => d.User)
                .Where(d => d.User.IsActive)
                .AsNoTracking()
                .ToListAsync();
        }


    }


}
