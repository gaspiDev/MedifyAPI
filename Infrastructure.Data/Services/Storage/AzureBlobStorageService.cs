using Azure.Storage.Blobs;
using Core.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;


namespace Infrastructure.Data.Services.Storage
{
    public class AzureBlobStorageService : IStorageService
    {
        private readonly BlobContainerClient _container;

        public AzureBlobStorageService(IConfiguration config)
        {
            var connectionString = config["AzureBlob:ConnectionString"];
            var containerName = config["AzureBlob:ContainerName"];

            var serviceClient = new BlobServiceClient(connectionString);
            _container = serviceClient.GetBlobContainerClient(containerName);

            _container.CreateIfNotExists();
        }

        public async Task<string> UploadStudyFile(IFormFile file)
        {
            var blobName = $"{Guid.NewGuid()}_{file.FileName}";
            var blobClient = _container.GetBlobClient(blobName);

            using var stream = file.OpenReadStream();
            await blobClient.UploadAsync(stream);

            return blobClient.Uri.ToString();
        }
    }
}