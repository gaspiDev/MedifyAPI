using Core.Application.DTOs.DoctorDTO;
using Core.Application.DTOs.PatientDTO;

namespace Core.Application.Interfaces
{
    public interface IDoctorService
    {
        Task<DoctorForViewDto?> ReadById(Guid id);
        Task<IEnumerable<DoctorForViewDto>?> ReadDoctors();
        Task<DoctorForViewDto?> ReadByUserIdAsync(Guid userId);
        Task<IEnumerable<PatientForViewDto>?> ReadPatientsByDoctor(Guid doctorId);
        Task<string?> CreateDoctorAsync(DoctorForCreationDto dto);
        Task<Guid?> UpdateDoctorAsync(Guid id, DoctorForUpdateDto dto);
    }
}