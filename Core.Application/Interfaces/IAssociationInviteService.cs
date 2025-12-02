using Core.Application.DTOs.Association;
using Core.Application.DTOs.AssociationDTO;

namespace Core.Application.Interfaces
{
    public interface IAssociationInviteService
    {
        Task<AssociationForViewDto> AcceptInviteAsync(AcceptInvitationDto dto);
        Task<InviteForViewDto> CreateInviteByCodeAsync(CreateInvitationDto dto);
        Task<InviteForViewDto> CreateInviteByQrAsync(CreateInvitationDto dto);
        Task<InviteForViewDto?> ValidateByCodeAsync(string code);
        Task<InviteForViewDto?> ValidateByQrAsync(string token);
        Task<AssociationForViewDto> AcceptByCodeAsync(string inviteCode, string auth0Id);
        Task<AssociationForViewDto> AcceptByQrAsync(string qrToken, string auth0Id);
    }
}