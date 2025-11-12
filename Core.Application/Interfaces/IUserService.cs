using Core.Application.DTOs.User;

namespace Core.Application.Interfaces
{
    public interface IUserService
    {
        Task<string> CreateUserAsync(UserForCreationDto userDto, string auth0id);
    }
}