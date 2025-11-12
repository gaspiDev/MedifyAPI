using Core.Application.DTOs.User;

namespace Core.Application.Interfaces
{
    public interface IAuth0Service
    {
        Task<string> CreateUserAsSysAdmin(UserForCreationDto userDto);
        Task<HttpResponseMessage> DeleteUserAsync(string userId);
    }
}