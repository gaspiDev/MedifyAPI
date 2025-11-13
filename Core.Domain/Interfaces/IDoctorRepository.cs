using Core.Domain.Entities;

namespace Infrastructure.Data.Repositories
{
    public interface IDoctorRepository : IBaseRepository<Doctor>
    {
        Task<IEnumerable<Doctor>> ReadAllDoctorsAsync();
        Task<Doctor?> ReadByLicenseAsync(string licenseNumber);
        Task<int?> DeleteDoctor(Guid id);
        Task<IEnumerable<Patient>?> ReadPatientsByDoctorAsync(Guid doctorId);
        Task<IEnumerable<Patient>?> ReadPatientsNotAssociatedByDoctorAsync(Guid doctorId);
        Task<Doctor?> ReadById(Guid id);
        Task<Doctor?> ReadByDniAsync(int dni);
    }
}