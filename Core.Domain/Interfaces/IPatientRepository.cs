using Core.Domain.Entities;

namespace Infrastructure.Data.Repositories
{
    public interface IPatientRepository : IBaseRepository<Patient>
    {
        Task<IEnumerable<Patient>> ReadAllPatientsAsync();
        Task<Patient?> ReadByDniAsync(int dni);
        Task<Patient?> ReadUserByIdAsync(Guid Id);
        Task<IEnumerable<Doctor?>> ReadDoctorsByPatientAsync(Guid patientId);
        Task<int?> DeletePatient(Guid id);
    }
}