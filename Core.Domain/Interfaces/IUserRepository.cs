using Core.Domain.Entities;

namespace Infrastructure.Data.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<int> DeleteUserAsync(Guid id);
        Task<User?> ReadUserByEmailAsync(string email);
        Task<IEnumerable<User?>> ReadUsers();
        Task<User?> ReadByAuth0IdAsync(string auth0Id);
    }
}