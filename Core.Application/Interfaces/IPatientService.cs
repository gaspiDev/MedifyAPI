using Core.Application.DTOs.DoctorDTO;
using Core.Application.DTOs.PatientDTO;

namespace Core.Application.Interfaces
{
    public interface IPatientService
    {
        Task<PatientForViewDto?> ReadById(Guid id);
        Task<IEnumerable<DoctorForViewDto>?> ReadDoctorsByPatient(Guid patientId);
        Task<IEnumerable<PatientForViewDto>?> ReadPatients();
        Task<string?> CreatePatientAsync(PatientForCreationDto patientForCreationDto);
    }
}