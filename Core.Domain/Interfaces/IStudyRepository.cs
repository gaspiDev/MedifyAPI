using Core.Domain.Entities;

namespace Infrastructure.Data.Repositories
{
    public interface IStudyRepository : IBaseRepository<Study>
    {
        Task<IEnumerable<Study>?> ReadStudies();
        Task<Study?> ReadStudyById(Guid id);
        Task <IEnumerable<Study>?> ReadStudiesByPatientId(Guid patientId);
    }
}