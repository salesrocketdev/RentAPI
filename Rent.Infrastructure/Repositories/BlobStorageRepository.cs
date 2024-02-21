using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Rent.Domain.Interfaces.Repositories;

namespace Rent.Infrastructure.Repositories;

public class BlobStorageRepository : IBlobStorageRepository
{
    private readonly BlobServiceClient _blobServiceClient;

    public BlobStorageRepository(string connectionString)
    {
        _blobServiceClient = new BlobServiceClient(connectionString);
    }

    public async Task<string> UploadBlobAsync(MemoryStream memoryStream, string blobName)
    {
        BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient("demo");

        BlobClient blobClient = containerClient.GetBlobClient(blobName);

        // Cria um objeto BlobHttpHeaders com o ContentType definido como "image/jpeg"
        BlobHttpHeaders headers = new() { ContentType = "image/jpeg" };

        // Faz o upload do arquivo com os headers definidos
        await blobClient.UploadAsync(memoryStream, headers);

        return blobClient.Uri.AbsoluteUri;
    }

    public async Task<IEnumerable<string>> ListBlobsAsync()
    {
        BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient("demo");
        List<string> blobNames = new();

        await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
        {
            blobNames.Add(blobItem.Name);
        }

        return blobNames;
    }
}
