using Core.Domain.Entities;

namespace Infrastructure.Data.Repositories
{
    public interface IUserRepository
    {
        Task<int> DeleteUserAsync(Guid id);
        Task<User?> ReadUserByEmailAsync(string email);
        Task<IEnumerable<User?>> ReadUsers();
    }
}