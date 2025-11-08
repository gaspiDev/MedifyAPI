using Core.Domain.Entities;

namespace Infrastructure.Data.Repositories
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> ReadAllWithUsersAsync();
        Task<Doctor?> ReadByLicenseAsync(string licenseNumber);
        Task<int?> DeleteDoctor(Guid id);
    }
}