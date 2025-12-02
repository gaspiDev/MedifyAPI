using Core.Application.DTOs.AssociationDTO;
using Core.Application.DTOs.PatientDTO;

namespace Core.Application.Interfaces
{
    public interface IDoctorPatientService
    {
        Task<IEnumerable<PatientForViewDto>?> ReadPatientsNotAssociatedWithDoctorAsync(Guid doctorId);
        Task<AssociationForViewDto> CreateAssociationAsync(AssociationAsSysAdminDto dto);
        Task<AssociationForViewDto?> UnassignAssociationAsync(Guid associationId);
    }
}