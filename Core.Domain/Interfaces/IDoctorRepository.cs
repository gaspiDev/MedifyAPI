using Core.Domain.Entities;

namespace Infrastructure.Data.Repositories
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetAllWithUsersAsync();
        Task<Doctor?> GetByLicenseAsync(string licenseNumber);
    }
}