using Domain.Abstractions.Services;
using Domain.Utils;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;

namespace Infrastructure.FileStorage;

public class MinioFileStorageService: IFileStorageService
{
    private readonly IMinioClient _minioClient;
    private readonly ILogger<MinioFileStorageService> _logger;
    
    public MinioFileStorageService(IOptions<MinioOptions> options, ILogger<MinioFileStorageService> logger)
    {
        var settings = options.Value;
        _logger = logger;
        var endpoint = settings.Endpoint ?? throw new ArgumentNullException(nameof(settings.Endpoint));
        
        _minioClient = new MinioClient()
            .WithEndpoint(endpoint)
            .WithCredentials(settings.AccessKey, settings.SecretKey)
            .WithSSL(false)
            .Build();
    }
    
    public async Task<Result<string>> UploadFileAsync(Stream fileStream, string fileName, string contentType, 
        string bucketName, CancellationToken cancellationToken = default)
    {
        try
        {
            await EnsureBucketExistsAsync(bucketName, cancellationToken);

            var putObjectArgs = new PutObjectArgs()
                .WithBucket(bucketName)
                .WithObject(fileName)
                .WithStreamData(fileStream)
                .WithObjectSize(fileStream.Length)
                .WithContentType(contentType);

            await _minioClient.PutObjectAsync(putObjectArgs, cancellationToken);

            var filePath = $"localhost:9000/{bucketName}/{fileName}";
            return Result<string>.Success(filePath);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error receiving the file: {@ex}", ex.Message);
            return Result<string>.Failure($"Error receiving the file: {ex.Message}")!;
        }
    }

    public async Task<Result<Stream?>?> GetFileAsync(string fileName, string bucketName, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            var ms = new MemoryStream();

            var args = new GetObjectArgs()
                .WithBucket(bucketName)
                .WithObject(fileName)
                .WithCallbackStream(stream => stream.CopyTo(ms));

            await _minioClient.GetObjectAsync(args, cancellationToken);

            ms.Position = 0;
            return Result<Stream?>.Success(ms);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error receiving the file: {@ex}", ex.Message);
            return Result<Stream?>.Failure($"Error receiving the file: {ex.Message}");
        }
    }

    public async Task<Result> DeleteFileAsync(string fileName, string bucketName,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var args = new RemoveObjectArgs()
                .WithBucket(bucketName)
                .WithObject(fileName);

            await _minioClient.RemoveObjectAsync(args, cancellationToken);
            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError("Error receiving the file: {@ex}", ex.Message);
            return Result.Failure($"Error deleting the file: {ex.Message}");
        }
    }

    private async Task EnsureBucketExistsAsync(string bucketName, CancellationToken cancellationToken = default)
    {
        var existsArgs = new BucketExistsArgs()
            .WithBucket(bucketName);
        
        bool exists = await _minioClient.BucketExistsAsync(existsArgs, cancellationToken);

        if (!exists)
        {
            var makeArgs = new MakeBucketArgs().WithBucket(bucketName);
            await _minioClient.MakeBucketAsync(makeArgs, cancellationToken);
        }
    }
}