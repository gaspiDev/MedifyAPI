using Core.Application.DTOs.User;
using Core.Application.DTOs.UserDTO;

namespace Core.Application.Interfaces
{
    public interface IUserService
    {
        Task<string> CreateUserAsync(UserForCreationDto userDto, string auth0id);
        Task<UserForViewDto?> ReadUserByEmail(string email);
        Task<bool> DeleteUserAsync(Guid id);
        Task<UserForViewDto?> ReadByAuth0IdAsync(string auth0id);
    }
}