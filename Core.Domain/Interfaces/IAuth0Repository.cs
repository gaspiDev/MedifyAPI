
namespace Infrastructure.Data.Repositories
{
    public interface IAuth0Repository
    {
        Task<string> CreateUserAsync(string email, string password);
        Task<HttpResponseMessage> DeleteUserAsync(string auth0Id);
    }
}