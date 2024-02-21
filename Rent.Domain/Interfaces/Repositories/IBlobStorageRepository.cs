namespace Rent.Domain.Interfaces.Repositories;

public interface IBlobStorageRepository
{
    Task<string> UploadBlobAsync(MemoryStream memoryStream, string blobName);
    Task<IEnumerable<string>> ListBlobsAsync();
}
