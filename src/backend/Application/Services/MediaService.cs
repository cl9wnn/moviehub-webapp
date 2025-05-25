using Domain.Abstractions.Services;
using Domain.Utils;

namespace Application.Services;

public class MediaService(IFileStorageService minioService): IMediaService
{
    public async Task<Result<string>> UploadMediaFile(Stream stream, string objectName, string contentType, string bucketName)
    {
        var urlResult = await minioService.UploadFileAsync(stream, objectName, contentType, bucketName);
        
        return urlResult.IsSuccess
            ? Result<string>.Success(urlResult.Data)
            : Result<string>.Failure(urlResult.ErrorMessage!)!;
    }
}