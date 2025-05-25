using Domain.Utils;

namespace Domain.Abstractions.Services;

public interface IMediaService
{
    Task<Result<string>> UploadMediaFile(Stream stream, string objectName, string contentType, string bucketName);
}