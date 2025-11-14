using Core.Application.DTOs.AssociationDTO;

namespace Core.Application.Interfaces
{
    public interface IDoctorPatientService
    {
        Task<AssociationForViewDto> CreateAssociationAsync(AssociationAsSysAdminDto dto);
        Task<AssociationForViewDto?> UnassignAssociationAsync(Guid associationId);
    }
}