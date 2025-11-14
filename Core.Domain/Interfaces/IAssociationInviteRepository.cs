using Core.Domain.Entities;

namespace Infrastructure.Data.Repositories
{
    public interface IAssociationInviteRepository : IBaseRepository<AssociationInvite>
    {
        Task<AssociationInvite?> ReadByCodeAsync(string code);
        Task<AssociationInvite?> ReadQRAsync(string qrToken);
    }
}