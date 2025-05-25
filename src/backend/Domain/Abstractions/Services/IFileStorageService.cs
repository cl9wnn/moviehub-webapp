using Domain.Utils;

namespace Domain.Abstractions.Services;

public interface IFileStorageService    
{
    Task<Result<string>> UploadFileAsync(Stream fileStream, string fileName, string contentType, string bucketName, 
        CancellationToken cancellationToken = default);
    Task<Result<Stream?>?> GetFileAsync(string fileName, string bucketName, CancellationToken cancellationToken = default);
    Task<Result> DeleteFileAsync(string fileName, string bucketName, CancellationToken cancellationToken = default);
}