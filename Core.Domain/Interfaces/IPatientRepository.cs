using Core.Domain.Entities;

namespace Infrastructure.Data.Repositories
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> ReadAllWithUsersAsync();
        Task<Patient?> ReadByDniAsync(int dni);
        Task<Patient?> ReadByUserIdAsync(Guid userId);
        Task<int?> DeletePatient(Guid id);
    }
}