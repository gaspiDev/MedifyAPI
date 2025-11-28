using Microsoft.AspNetCore.Http;


namespace Core.Application.Interfaces
{
    public interface IStorageService
    {
        Task<string> UploadStudyFile(IFormFile file);
    }
}
