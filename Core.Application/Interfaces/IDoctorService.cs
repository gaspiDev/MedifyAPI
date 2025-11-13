using Core.Application.DTOs.DoctorDTO;
using Core.Application.DTOs.PatientDTO;

namespace Core.Application.Interfaces
{
    public interface IDoctorService
    {
        Task<DoctorForViewDto?> ReadById(Guid id);
        Task<IEnumerable<DoctorForViewDto>?> ReadDoctors();
        Task<IEnumerable<PatientForViewDto>?> ReadPatientsByDoctor(Guid doctorId);
        Task<IEnumerable<PatientForViewDto>?> ReadPatientsNotAssociatedByDoctorAsync(Guid id);
        Task<string?> CreateDoctorAsync(DoctorForCreationDto dto);
    }
}