using Core.Domain.Entities;

namespace Infrastructure.Data.Repositories
{
    public interface IDoctorPatientRepository : IBaseRepository<DoctorPatient>
    {
        Task<DoctorPatient?> ReadActiveAssociationAsync();
        Task<DoctorPatient?> ReadAssociationByIdAsync(Guid id);
        Task<DoctorPatient?> ReadExistingAssociationsAsync(Guid doctorId, Guid patientId);
        Task<DoctorPatient?> ReadInactiveAsync(Guid doctorId, Guid patientId);
        Task<IEnumerable<DoctorPatient>?> ReadAllActiveAssociationsAsync();
        Task<IEnumerable<DoctorPatient>?> ReadAssociationsByDoctorAsync(Guid doctorId);
        Task<IEnumerable<DoctorPatient>?> ReadAssociationsByPatientAsync(Guid patientId);
        Task<Guid?> DeleteAssociations(Guid id);
    }
}