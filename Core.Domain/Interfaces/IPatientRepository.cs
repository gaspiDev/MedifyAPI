using Core.Domain.Entities;

namespace Infrastructure.Data.Repositories
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetAllWithUsersAsync();
        Task<Patient?> GetByDniAsync(int dni);
        Task<Patient?> GetByUserIdAsync(Guid userId);
    }
}